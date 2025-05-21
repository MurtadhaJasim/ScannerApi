using WIA;

namespace ScannerMiddlewareApi.Interfaces
{
    public interface ISetScanDimensions
    {
      void ConfigureScanDimensions(IProperties props, string paperSize);
    }
}
