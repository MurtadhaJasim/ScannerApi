using ScannerMiddlewareApi.Interfaces;
using WIA;

namespace ScannerMiddlewareApi.Services
{
    public class SetWIAProperty : ISetWIAProperty
    {
        public void SetWIAProp(IProperties properties, object propName, object propValue)
        {
            foreach (Property prop in properties)
            {
                if (prop.PropertyID == Convert.ToInt32(propName))
                {
                    prop.set_Value(ref propValue);
                    return;
                }
            }
        }
    }
}
