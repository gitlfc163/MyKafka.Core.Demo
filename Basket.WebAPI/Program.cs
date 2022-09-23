var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//×¢ÈëCAP
builder.Services.AddCap(x =>
{
    //x.UseMongoDB("mongodb://account:password@mongodb-server:2717/products?authSource=admin");
    x.UseMongoDB("mongodb://localhost:2717/products?authSource=admin");
    x.UseKafka("kafka1:9091,kafka2:9092,kafka3:9093");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
