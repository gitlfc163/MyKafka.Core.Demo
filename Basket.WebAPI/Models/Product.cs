﻿namespace Basket.WebAPI.Models;

public class Product
{
    public string Id { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string Introduction { get; set; }
}
