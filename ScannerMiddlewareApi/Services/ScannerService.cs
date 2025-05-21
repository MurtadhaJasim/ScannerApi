using ScannerMiddlewareApi.Data;
using ScannerMiddlewareApi.Interfaces;
using ScannerMiddlewareApi.Models;
using WIA;

public class ScannerService : IScannerService
{
    private readonly ApplicationDbContext _db;
    private readonly IDeviceService _deviceService;
    private readonly ISetScanDimensions _setScanDimensions;
    private readonly ISetWIAProperty _setWIAProperty;

    public ScannerService(ApplicationDbContext db,
        IDeviceService deviceService,
        ISetScanDimensions setScanDimensions,
        ISetWIAProperty setWIAProperty)
    {
        _db = db;
        _deviceService = deviceService;
        _setScanDimensions = setScanDimensions;
        _setWIAProperty = setWIAProperty;
    }

    public async Task<ScanResult> ScanFlatbed(int deviceIndex, string paperSize = "A4")
    {
        return await PerformScanAsync(deviceIndex, "Flatbed", paperSize, null);
    }

    public async Task<ScanResult> ScanFeeder(int deviceIndex, string paperSize = "A4")
    {
        string feederCommand = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        return await PerformScanAsync(deviceIndex, "Feeder", paperSize, feederCommand);
    }

    private async Task<ScanResult> PerformScanAsync(int deviceIndex, string scanType, string paperSize, string? command)
    {
        try
        {
            var device = _deviceService.GetDevice(deviceIndex);
            var item = device.Items[1];

            _setWIAProperty.SetWIAProp(item.Properties, "6146", 2);   // Color
            _setWIAProperty.SetWIAProp(item.Properties, "6147", 300); // DPI X
            _setWIAProperty.SetWIAProp(item.Properties, "6148", 300); // DPI Y

            _setScanDimensions.ConfigureScanDimensions(item.Properties, paperSize);

            ImageFile image = command == null
                ? (ImageFile)item.Transfer("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}") // Flatbed
                : (ImageFile)device.ExecuteCommand(command);                         // Feeder

            byte[] imageData = (byte[])image.FileData.get_BinaryData();

            var entity = new ScannedImage
            {
                ScanType = scanType,
                Timestamp = DateTime.UtcNow,
                ContentType = "image/jpeg",
                ImageData = imageData
            };

            _db.ScannedImages.Add(entity);
            await _db.SaveChangesAsync();

            return new ScanResult
            {
                Success = true,
                FilePath = $"DB:{entity.Id}"
            };
        }
        catch (Exception ex)
        {
            return new ScanResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
