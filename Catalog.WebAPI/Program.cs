using AutoMapper;
using Catalog.WebAPI;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using static Confluent.Kafka.ConfigPropertyNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//注入CAP
builder.Services.AddCap(x =>
{
    //x.UseMongoDB("mongodb://account:password@mongodb-server:2717/products?authSource=admin");
    x.UseMongoDB("mongodb://localhost:2717/products?authSource=admin");
    x.UseKafka("kafka1:9091,kafka2:9092,kafka3:9093");
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

IList<Product> GetProductList()
{
    IList<Product> products = new List<Product>
        {
            new Product { Id = "0001", Name = "电动牙刷A", Price = 99.90M,  Introduction = "暂无介绍" },
            new Product { Id = "0002", Name = "电动牙刷B", Price = 199.90M,  Introduction = "暂无介绍" },
            new Product { Id = "0003", Name = "洗衣机A", Price = 2999.90M,  Introduction = "暂无介绍" },
            new Product { Id = "0004", Name = "洗衣机B", Price = 3999.90M,  Introduction = "暂无介绍" },
            new Product { Id = "0005", Name = "电视机A", Price = 1899.90M,  Introduction = "暂无介绍" },
        };
    return products;
}


app.MapGet("/Get", () =>
{
    //return _mapper.Map<IList<ProductDTO>>(GetProductList());
    return GetProductList();
});

app.MapPut("/UpdatePrice", async (ICapPublisher _publisher,string id, decimal newPrice) =>
{
    // 业务代码
    var product = GetProductList().FirstOrDefault(p => p.Id == id);
    product.Price = newPrice;

    // 发布消息
    await _publisher.PublishAsync("ProductPriceChanged",
        new ProductDTO { Id = product.Id, Name = product.Name, Price = product.Price });

});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

