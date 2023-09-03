using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using Pustok.Database.Models;

namespace Pustok.Controllers;

[Route("basket")]
public class BasketController : Controller
{
    private readonly PustokDbContext _dbContext;

    public BasketController(PustokDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add-product/{productId}")]
    public IActionResult AddProduct(int productId)
    {
        var product = _dbContext.Products.SingleOrDefault(p => p.Id == productId);
        if (product == null)
        {
            return NotFound();
        }

        //TODO : We will provide user id in future lessons
        var basket = _dbContext.Baskets.SingleOrDefault();
        if (basket == null)
        {
            basket = new Basket();
            _dbContext.Baskets.Add(basket);
        }

        var productColor = _dbContext.ProductColors
            .First(pc => pc.ProductId == product.Id);
        var productSize = _dbContext.ProductSizes
            .First(ps => ps.ProductId == product.Id);

        var basketItem = _dbContext.BasketItems.SingleOrDefault(bi =>
            bi.Basket == basket &&
            bi.ProductId == product.Id &&
            bi.SizeId == productSize.SizeId &&
            bi.ColorId == productColor.ColorId);

        if (basketItem == null)
        {
            basketItem = new BasketItem
            {
                ProductId = product.Id,
                ColorId = productColor.ColorId,
                SizeId = productSize.SizeId,
                Basket = basket,
                Quantity = 1,
            };

            _dbContext.BasketItems.Add(basketItem);
        }
        else
        {
            basketItem.Quantity++;
        }

        _dbContext.SaveChanges();

        return Ok();
    }

    public IActionResult Index()
    {
        return View();
    }
}
