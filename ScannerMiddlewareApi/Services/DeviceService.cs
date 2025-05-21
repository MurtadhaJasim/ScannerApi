using WIA;

public class DeviceService : IDeviceService
{
    public List<string> GetAvailableDevices()
    {
        var manager = new DeviceManager();
        var devices = new List<string>();

        for (int i = 1; i <= manager.DeviceInfos.Count; i++) // تبدأ من 1 حسب WIA
        {
            var info = manager.DeviceInfos[i];
            var nameProperty = info.Properties["Name"];
            var nameValue = nameProperty.get_Value(); // Use the accessor method to retrieve the value
            devices.Add($"{i}: {nameValue}");
        }

        return devices;
    }

    public Device GetDevice(int deviceIndex)
    {
        var manager = new DeviceManager();
        if (manager.DeviceInfos.Count == 0)
            throw new Exception("لم يتم العثور على أي جهاز سكنر.");

        if (deviceIndex < 1 || deviceIndex > manager.DeviceInfos.Count)
            throw new ArgumentOutOfRangeException(nameof(deviceIndex), "رقم الجهاز غير صالح.");

        var info = manager.DeviceInfos[deviceIndex];
        return info.Connect();
    }
}
