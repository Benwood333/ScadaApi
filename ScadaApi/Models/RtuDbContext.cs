using Microsoft.EntityFrameworkCore;

namespace ScadaApi.Models
{
    public class RtuDbContext : DbContext
    {
        public RtuDbContext(DbContextOptions<RtuDbContext> options)
            : base(options)
        {
       
        }

        public DbSet<Rtu> Rtus { get; set; } = null!;

        public DbSet<Point> Points { get; set; } = null!;
    }
}
