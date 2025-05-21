using WIA;

namespace ScannerMiddlewareApi.Interfaces
{
    public interface ISetWIAProperty
    {
        public void SetWIAProp(IProperties properties, object propName, object propValue);
    }
}
