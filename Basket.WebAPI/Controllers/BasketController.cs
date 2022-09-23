using Basket.WebAPI.Models;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace Basket.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private static readonly IList<MyBasketDTO> Baskets = new List<MyBasketDTO>
    {
        new MyBasketDTO { UserId = "U001", Catalogs = new List<Catalog>
            {
                new Catalog { Product = new ProductDTO { Id = "0001", Name = "电动牙刷A", Price = 99.90M }, Count = 2 },
                new Catalog { Product = new ProductDTO { Id = "0005", Name = "电视机A", Price = 1899.90M }, Count = 1 },
            }
        },
        new MyBasketDTO { UserId = "U002", Catalogs = new List<Catalog>
            {
                new Catalog { Product = new ProductDTO { Id = "0002", Name = "电动牙刷B", Price = 199.90M }, Count = 2 },
                new Catalog { Product = new ProductDTO { Id = "0004", Name = "洗衣机B", Price = 3999.90M }, Count = 1 },
            }
        }
    };

    [HttpGet]
    public IList<MyBasketDTO> Get()
    {
        return Baskets;
    }

    [NonAction]
    [CapSubscribe("ProductPriceChanged")]
    public async Task RefreshBasketProductPrice(ProductDTO productDTO)
    {
        if (productDTO == null)
            return;

        foreach (var basket in Baskets)
        {
            foreach (var catalog in basket.Catalogs)
            {
                if (catalog.Product.Id == productDTO.Id)
                {
                    catalog.Product.Price = productDTO.Price;
                    break;
                }
            }
        }

        await Task.CompletedTask;
    }
}
