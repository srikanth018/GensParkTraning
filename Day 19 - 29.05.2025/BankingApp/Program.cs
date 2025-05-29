using System.Text.Json.Serialization;
using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Models;
using BankApp.Repositories;
using BankApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<BankingContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddTransient<IRepository<int, AccountHolder>, AccountHolderRepository>();
builder.Services.AddTransient<IRepository<int, Address>, AddressRepository>();
builder.Services.AddTransient<IRepository<int, AccountDetail>, AccountDetailsRepository>();
builder.Services.AddTransient<IAccountHolderService, AccountHolderService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; 
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();

