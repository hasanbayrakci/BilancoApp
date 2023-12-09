using Microsoft.EntityFrameworkCore;

namespace BilancoApp.Model
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<BilancoApp.Model.Kalemler> Kalemler { get; set; } = default!;
        public DbSet<GelirGider> GelirGider { get; set; } = default!;

    }
}
