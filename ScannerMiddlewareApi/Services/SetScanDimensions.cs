using ScannerMiddlewareApi.Interfaces;
using WIA;

namespace ScannerMiddlewareApi.Services
{
    public class SetScanDimensions : ISetScanDimensions
    {
        private readonly ISetWIAProperty _setWIAProperty;
        public SetScanDimensions(ISetWIAProperty setWIAProperty)
        {
            _setWIAProperty = setWIAProperty;
        }
        public void ConfigureScanDimensions(IProperties props, string paperSize)
        {
            int dpi = 300;
            double widthInches = 8.27;
            double heightInches = 11.69;

            switch (paperSize.ToUpper())
            {
                case "A3":
                    widthInches = 11.69;
                    heightInches = 16.54;
                    break;
                case "A4":
                    widthInches = 8.27;
                    heightInches = 11.69;
                    break;
                case "A5":
                    widthInches = 5.83;
                    heightInches = 8.27;
                    break;
                case "A6":
                    widthInches = 4.13;
                    heightInches = 5.83;
                    break;
                case "B5":
                    widthInches = 6.93;
                    heightInches = 9.84;
                    break;
                case "B4":
                    widthInches = 9.84;
                    heightInches = 13.9;
                    break;
                case "LETTER":
                    widthInches = 8.5;
                    heightInches = 11;
                    break;
                case "LEGAL":
                    widthInches = 8.5;
                    heightInches = 14;
                    break;
                case "EXECUTIVE":
                    widthInches = 7.25;
                    heightInches = 10.5;
                    break;
                case "TABLOID":
                    widthInches = 11;
                    heightInches = 17;
                    break;
                case "LEDGER":
                    widthInches = 17;
                    heightInches = 11;
                    break;
                default:
                    widthInches = 8.27;
                    heightInches = 11.69;
                    break;
            }

            int widthPixels = (int)(widthInches * dpi);
            int heightPixels = (int)(heightInches * dpi);

            _setWIAProperty.SetWIAProp(props, "6151", 0); // Start X
            _setWIAProperty.SetWIAProp(props, "6152", 0); // Start Y
            _setWIAProperty.SetWIAProp(props, "6154", widthPixels);  // Width
            _setWIAProperty.SetWIAProp(props, "6155", heightPixels); // Height
        }
    }
}
