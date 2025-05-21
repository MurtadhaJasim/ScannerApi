namespace ScannerMiddlewareApi.Models
{
    public class ScannedImage
    {
        public int Id { get; set; }
        public string ScanType { get; set; } = "";         // Flatbed أو Feeder
        public DateTime Timestamp { get; set; }            // وقت المسح
        public string ContentType { get; set; } = "";      // نوع الصورة
        public byte[] ImageData { get; set; } = Array.Empty<byte>(); // بيانات الصورة
    }
}
