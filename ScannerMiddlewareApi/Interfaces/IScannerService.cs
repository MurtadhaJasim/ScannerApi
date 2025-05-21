using ScannerMiddlewareApi.Models;

namespace ScannerMiddlewareApi.Interfaces
{
    public interface IScannerService
    {

        Task<ScanResult> ScanFlatbed(int deviceIndex, string paperSize = "A4");
        Task<ScanResult> ScanFeeder(int deviceIndex, string paperSize = "A4");

    }

}

