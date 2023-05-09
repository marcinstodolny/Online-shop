using Domain;
using Microsoft.EntityFrameworkCore;

public interface ICodecoolshopContext
{
    DbSet<Product> Products { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    DbSet<Supplier> Suppliers { get; }
    public DbSet<Order> Orders { get; set; }
}
