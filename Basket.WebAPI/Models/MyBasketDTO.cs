namespace Basket.WebAPI.Models;

public class MyBasketDTO
{
    public string UserId { get; set; }
    public IList<Catalog> Catalogs { get; set; }
}
