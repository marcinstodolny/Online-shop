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

        public static void IfDbEmptyAddNewItems(CodecoolshopContext context)
        {
            if (!context.Products.Any())
            {
                var amazon = new Supplier { Name = "Amazon", Description = "Digital content and services" };
                var lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
                var apple = new Supplier { Name = "Apple", Description = "Electronics" };
                var samsung = new Supplier { Name = "Samsung", Description = "Electronics" };

                context.Suppliers.Add(amazon);
                context.Suppliers.Add(lenovo);
                context.Suppliers.Add(apple);
                context.Suppliers.Add(samsung);

                var tablet = new ProductCategory { Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
                var laptop = new ProductCategory { Name = "Laptop", Department = "Hardware", Description = "A laptop is a portable personal computer with a clamshell form factor, suitable for mobile use and with a built-in keyboard and display." };
                var smartphone = new ProductCategory { Name = "Smartphone", Department = "Hardware", Description = "A smartphone is a mobile device that combines cellular and mobile computing functions into one unit." };

                context.ProductCategories.Add(tablet);
                context.ProductCategories.Add(laptop);
                context.ProductCategories.Add(smartphone);

                // Add products conditionally

                var amazonFire = new Product
                {
                    Name = "Amazon Fire",
                    DefaultPrice = 49.9m,
                    Currency = "USD",
                    Description =
                        "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.",
                    ProductCategory = tablet,
                    Supplier = amazon
                };
                var lenovoIdeaPad = new Product
                {
                    Name = "Lenovo IdeaPad Miix 700",
                    DefaultPrice = 479.0m,
                    Currency = "USD",
                    Description =
                        "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.",
                    ProductCategory = tablet,
                    Supplier = lenovo
                };
                var amazonFireHd = new Product
                {
                    Name = "Amazon Fire HD 8",
                    DefaultPrice = 89.0m,
                    Currency = "USD",
                    Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.",
                    ProductCategory = tablet,
                    Supplier = amazon
                };
                var macbookPro = new Product
                {
                    Name = "MacBook Pro",
                    DefaultPrice = 1299.0m,
                    Currency = "USD",
                    Description = "The MacBook Pro is a line of Macintosh portable computers introduced by Apple Inc. in January 2006.",
                    ProductCategory = laptop,
                    Supplier = apple
                };
                var galaxyS21 = new Product
                {
                    Name = "Samsung Galaxy S21",
                    DefaultPrice = 799.0m,
                    Currency = "USD",
                    Description = "The Samsung Galaxy S21 is an Android-based smartphone designed, developed, and marketed by Samsung Electronics.",
                    ProductCategory = smartphone,
                    Supplier = samsung
                };
                var iPhone12 = new Product
                {
                    Name = "Apple iPhone 12",
                    DefaultPrice = 699.0m,
                    Currency = "USD",
                    Description = "The iPhone 12 is a smartphone designed, developed, and marketed by Apple Inc.",
                    ProductCategory = smartphone,
                    Supplier = apple
                };

                // ... add more products as needed ...
                context.Products.Add(amazonFire);
                context.Products.Add(lenovoIdeaPad);
                context.Products.Add(amazonFireHd);
                context.Products.Add(macbookPro);
                context.Products.Add(galaxyS21);
                context.Products.Add(iPhone12);
                context.SaveChanges();
            }
        }
    }
}