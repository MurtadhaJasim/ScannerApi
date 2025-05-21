using Microsoft.EntityFrameworkCore;
using ScannerMiddlewareApi.Models;

namespace ScannerMiddlewareApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }

        public DbSet<ScannedImage> ScannedImages { get; set; }
    }
}
