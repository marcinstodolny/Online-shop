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
        //public DbSet<Cart> Carts { get; set; }
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

        public void IfDbEmptyAddNewItems(CodecoolshopContext context)
        {
            if (!context.Products.Any())
            {
                var amazon = new Supplier { Name = "Amazon", Description = "Digital content and services" };
                var lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
                context.Suppliers.Add(amazon);
                context.Suppliers.Add(lenovo);
                var tablet = new Domain.ProductCategory { Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
                context.ProductCategories.Add(tablet);

                // Add Cart
                //var cart = new Cart()
                //{
                //    Items = new List<Item>()
                //};

                // Add products conditionally

                var amazonFire = new Domain.Product
                {
                    Name = "Amazon Fire",
                    DefaultPrice = 49.9m,
                    Currency = "USD",
                    Description =
                        "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.",
                    ProductCategory = tablet,
                    Supplier = amazon
                };
                var lenovoIdeaPad = new Domain.Product
                {
                    Name = "Lenovo IdeaPad Miix 700",
                    DefaultPrice = 479.0m,
                    Currency = "USD",
                    Description =
                        "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.",
                    ProductCategory = tablet,
                    Supplier = lenovo
                };
                var amazonFireHd = new Domain.Product
                {
                    Name = "Amazon Fire HD 8",
                    DefaultPrice = 89.0m,
                    Currency = "USD",
                    Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.",
                    ProductCategory = tablet,
                    Supplier = amazon
                };
                // ... add more products as needed ...
                context.Products.Add(amazonFire);
                context.Products.Add(lenovoIdeaPad);
                context.Products.Add(amazonFireHd);
                context.SaveChanges();
            }
        }
    }
}