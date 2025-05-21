namespace ScannerMiddlewareApi.Models
{
    public class ScanResult
    {
        public bool Success { get; set; }
        public string? FilePath { get; set; }      // مثال: DB:123
        public string? ErrorMessage { get; set; }
    }
}
