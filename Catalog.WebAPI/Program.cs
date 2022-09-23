using AutoMapper;
using Catalog.WebAPI;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using static Confluent.Kafka.ConfigPropertyNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//ע��CAP
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
            new Product { Id = "0001", Name = "�綯��ˢA", Price = 99.90M,  Introduction = "���޽���" },
            new Product { Id = "0002", Name = "�綯��ˢB", Price = 199.90M,  Introduction = "���޽���" },
            new Product { Id = "0003", Name = "ϴ�»�A", Price = 2999.90M,  Introduction = "���޽���" },
            new Product { Id = "0004", Name = "ϴ�»�B", Price = 3999.90M,  Introduction = "���޽���" },
            new Product { Id = "0005", Name = "���ӻ�A", Price = 1899.90M,  Introduction = "���޽���" },
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
    // ҵ�����
    var product = GetProductList().FirstOrDefault(p => p.Id == id);
    product.Price = newPrice;

    // ������Ϣ
    await _publisher.PublishAsync("ProductPriceChanged",
        new ProductDTO { Id = product.Id, Name = product.Name, Price = product.Price });

});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

