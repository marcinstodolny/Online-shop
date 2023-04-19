using Codecool.CodecoolShop.Controllers;
using Data;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Services
{
    public class ProductServiceSql : IProductServiceSql

    {
    private readonly ILogger<ProductController> _logger;
    private CodecoolshopContext _context;

    public ProductServiceSql(ILogger<ProductController> logger, CodecoolshopContext context)
    {
        _logger = logger;
        _context = context;

        context.IfDbEmptyAddNewItems(context);
    }

    public CodecoolshopContext GetContext() => _context;
    }
}
