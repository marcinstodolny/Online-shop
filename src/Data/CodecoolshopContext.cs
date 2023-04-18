using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class CodecoolshopContext : DbContext
    {
        //private const string ConnectionString = "Data Source=localhost;Database=Codecoolshop;Trust Server Certificate=true;MultipleActiveResultSets=true;Integrated Security=true";
        private readonly IConfiguration _configuration;
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public CodecoolshopContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("CodecoolShopConnectionString");
                optionsBuilder
                    .UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Employee>().Property(x => x.FirstName).IsRequired().HasColumnType("varchar(60)");
            //modelBuilder.Entity<Employee>().HasData();
            modelBuilder.Entity<Product>()
                .Property(p => p.DefaultPrice)
                .HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }
    }
}