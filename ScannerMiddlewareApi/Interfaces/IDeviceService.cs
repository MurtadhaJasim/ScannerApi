using WIA;

public interface IDeviceService
{
    Device GetDevice(int deviceIndex); // أو يمكنك استخدام ID
    List<string> GetAvailableDevices();
}
