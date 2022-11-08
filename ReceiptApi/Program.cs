using Microsoft.EntityFrameworkCore;
using ReceiptApi.Core.Validator;
using ReceiptApi.Data;
using ReceiptApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<IReceiptValidator, ReceiptValidator>();
builder.Services.AddScoped<IItemValidator, ItemValidator>();
builder.Services.AddDbContext<DataContext>(optins =>
{
    optins.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
