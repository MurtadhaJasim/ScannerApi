using Microsoft.AspNetCore.Mvc;
using ScannerMiddlewareApi.Data;
using ScannerMiddlewareApi.Interfaces;
using System.Text.RegularExpressions;

namespace ScannerMiddlewareApi.Controllers
{
    [ApiController]
    [Route("api/scan")]
    public class ScanController : ControllerBase
    {
        private readonly IScannerService _scannerService;
        private readonly ApplicationDbContext _db;
        private readonly IDeviceService _deviceService;

        public ScanController(IScannerService scannerService,
            ApplicationDbContext db,
            IDeviceService deviceService)
        {
            _scannerService = scannerService;
            _deviceService = deviceService;
            _db = db;
        }

        [HttpPost("flatbed")]
        public async Task<IActionResult> ScanFlatbed([FromQuery] int deviceIndex = 1, [FromQuery] string paperSize = "A4")
        {
            var result = await _scannerService.ScanFlatbed(deviceIndex, paperSize);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            var match = Regex.Match(result.FilePath, @"DB:(\d+)");
            if (!match.Success)
                return BadRequest("معرف الصورة غير صالح");

            int id = int.Parse(match.Groups[1].Value);
            var image = await _db.ScannedImages.FindAsync(id);

            if (image == null)
                return NotFound("الصورة غير موجودة");

            return File(image.ImageData, image.ContentType, $"scan_{id}.jpg");
        }

        [HttpPost("feeder")]
        public async Task<IActionResult> ScanFeeder([FromQuery] int deviceIndex = 1, [FromQuery] string paperSize = "A4")
        {
            var result = await _scannerService.ScanFeeder(deviceIndex, paperSize);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            var match = Regex.Match(result.FilePath, @"DB:(\d+)");
            if (!match.Success)
                return BadRequest("معرف الصورة غير صالح");

            int id = int.Parse(match.Groups[1].Value);
            var image = await _db.ScannedImages.FindAsync(id);

            if (image == null)
                return NotFound("الصورة غير موجودة");

            return File(image.ImageData, image.ContentType, $"scan_{id}.jpg");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var image = await _db.ScannedImages.FindAsync(id);
            if (image == null)
                return NotFound("الصورة غير موجودة");

            return File(image.ImageData, image.ContentType, $"scan_{id}.jpg");
        }

        [HttpGet("devices")]
        public IActionResult GetDevices()
        {
            var devices = _deviceService.GetAvailableDevices();
            return Ok(devices);
        }
    }
}
