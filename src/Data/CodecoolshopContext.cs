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
        public virtual DbSet<Product> Products { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public CodecoolshopContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public CodecoolshopContext()
        {
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
                var microsoft = new Supplier { Name = "Microsoft", Description = "Software and hardware" };
                var keurig = new Supplier { Name = "Keurig", Description = "Single-serve coffee and beverage systems" };
                var breville = new Supplier { Name = "Breville", Description = "Kitchen appliances" };

                context.Suppliers.Add(microsoft);
                context.Suppliers.Add(amazon);
                context.Suppliers.Add(lenovo);
                context.Suppliers.Add(apple);
                context.Suppliers.Add(samsung);
                context.Suppliers.Add(keurig);
                context.Suppliers.Add(breville);

                var tablet = new ProductCategory { Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
                var laptop = new ProductCategory { Name = "Laptop", Department = "Hardware", Description = "A laptop is a portable personal computer with a clamshell form factor, suitable for mobile use and with a built-in keyboard and display." };
                var smartphone = new ProductCategory { Name = "Smartphone", Department = "Hardware", Description = "A smartphone is a mobile device that combines cellular and mobile computing functions into one unit." };
                var coffeeMaker = new ProductCategory { Name = "Coffee Maker", Department = "Kitchen Appliances", Description = "Electronic devices used to brew coffee." };
                
                context.ProductCategories.Add(coffeeMaker);
                context.ProductCategories.Add(tablet);
                context.ProductCategories.Add(laptop);
                context.ProductCategories.Add(smartphone);

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
                        "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support",
                    ProductCategory = tablet,
                    Supplier = amazon
                };
                var lenovoIdeaPad = new Domain.Product
                {
                    Name = "Lenovo IdeaPad Miix 700",
                    DefaultPrice = 479.0m,
                    Currency = "USD",
                    Description =
                        "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand",
                    ProductCategory = tablet,
                    Supplier = lenovo
                };
                var amazonFireHd = new Domain.Product
                {
                    Name = "Amazon Fire HD 8",
                    DefaultPrice = 89.0m,
                    Currency = "USD",
                    Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption",
                    ProductCategory = tablet,
                    Supplier = amazon
                };
                var macbookPro = new Product
                {
                    Name = "MacBook Pro",
                    DefaultPrice = 1299.0m,
                    Currency = "USD",
                    Description = "The MacBook Pro is a line of Macintosh portable computers introduced by Apple Inc. in January 2006",
                    ProductCategory = laptop,
                    Supplier = apple
                };
                var galaxyS21 = new Product
                {
                    Name = "Samsung Galaxy S21",
                    DefaultPrice = 799.0m,
                    Currency = "USD",
                    Description = "The Samsung Galaxy S21 is an Android-based smartphone designed, developed, and marketed by Samsung Electronics",
                    ProductCategory = smartphone,
                    Supplier = samsung
                };
                var iPhone12 = new Product
                {
                    Name = "Apple iPhone 12",
                    DefaultPrice = 699.0m,
                    Currency = "USD",
                    Description = "The iPhone 12 is a smartphone designed, developed, and marketed by Apple Inc",
                    ProductCategory = smartphone,
                    Supplier = apple
                };
                var surfaceLaptop = new Product
                {
                    Name = "Microsoft Surface Laptop",
                    DefaultPrice = 999.0m,
                    Currency = "USD",
                    Description = "A slim, stylish laptop with a high-resolution touchscreen display and excellent battery life",
                    ProductCategory = laptop,
                    Supplier = microsoft
                };
                var macbookAir = new Product
                {
                    Name = "Apple MacBook Air",
                    DefaultPrice = 1199.0m,
                    Currency = "USD",
                    Description = "A lightweight, portable laptop with a high-resolution Retina display and powerful performance",
                    ProductCategory = laptop,
                    Supplier = apple
                };
                var surfacePro = new Product
                {
                    Name = "Microsoft Surface Pro",
                    DefaultPrice = 799.0m,
                    Currency = "USD",
                    Description = "A versatile 2-in-1 tablet and laptop with a high-resolution touchscreen display and excellent battery life",
                    ProductCategory = tablet,
                    Supplier = microsoft
                };
                var lenovoThinkPad = new Product
                {
                    Name = "Lenovo ThinkPad X1 Carbon",
                    DefaultPrice = 1299.0m,
                    Currency = "USD",
                    Description = "A durable, high-performance business laptop with a sleek design and powerful performance",
                    ProductCategory = laptop,
                    Supplier = lenovo
                };
                var appleiPadPro = new Product
                {
                    Name = "Apple iPad Pro",
                    DefaultPrice = 799.0m,
                    Currency = "USD",
                    Description = "A powerful tablet with a high-resolution display, advanced performance, and support for Apple Pencil",
                    ProductCategory = tablet,
                    Supplier = apple
                };
                var keurigKElite = new Product
                {
                    Name = "Keurig K-Elite",
                    DefaultPrice = 169.0m,
                    Currency = "USD",
                    Description = "A single-serve coffee maker with a strong brew setting and iced coffee capabilities",
                    ProductCategory = coffeeMaker,
                    Supplier = keurig
                };
                var brevilleBaristaExpress = new Product
                {
                    Name = "Breville Barista Express",
                    DefaultPrice = 599.0m,
                    Currency = "USD",
                    Description = "A semi-automatic espresso machine with a built-in conical burr grinder and precise temperature controls",
                    ProductCategory = coffeeMaker,
                    Supplier = breville
                };
                var keurigKMini = new Product
                {
                    Name = "Keurig K-Mini",
                    DefaultPrice = 79.0m,
                    Currency = "USD",
                    Description = "A compact single-serve coffee maker, perfect for small spaces and on-the-go brewing",
                    ProductCategory = coffeeMaker,
                    Supplier = keurig
                };
                var brevillePrecisionBrewer = new Product
                {
                    Name = "Breville Precision Brewer",
                    DefaultPrice = 279.0m,
                    Currency = "USD",
                    Description = "A versatile drip coffee maker with customizable brew settings and a built-in cold brew mode",
                    ProductCategory = coffeeMaker,
                    Supplier = breville
                };
                // ... add more products as needed ...
                context.Products.Add(keurigKElite);
                context.Products.Add(brevilleBaristaExpress);
                context.Products.Add(keurigKMini);
                context.Products.Add(brevillePrecisionBrewer);
                context.Products.Add(amazonFire);
                context.Products.Add(lenovoIdeaPad);
                context.Products.Add(amazonFireHd);
                context.Products.Add(macbookPro);
                context.Products.Add(galaxyS21);
                context.Products.Add(iPhone12);
                context.Products.Add(surfaceLaptop);
                context.Products.Add(macbookAir);
                context.Products.Add(surfacePro);
                context.Products.Add(lenovoThinkPad);
                context.Products.Add(appleiPadPro);
                context.SaveChanges();
            }
        }
    }
}