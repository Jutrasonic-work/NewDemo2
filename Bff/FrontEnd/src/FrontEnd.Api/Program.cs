using FrontEnd.Api.Features.Account;
using FrontEnd.Api.Features.Account.Endpoints;
using FrontEnd.Api.Features.Admin;
using FrontEnd.Api.Features.Admin.Endpoints;
using FrontEnd.Api.Features.Shop;
using FrontEnd.Api.Features.Shop.Endpoints;
using FrontEnd.Api.Services;
using FrontEnd.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add API Services
builder.Services.AddApiServices(builder.Configuration);

// Register use cases
builder.Services.AddAdminFeatures();
builder.Services.AddShopFeatures();
builder.Services.AddAccountFeatures();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map Endpoints
app.MapAccountEndpoints();
app.MapAdminEndpoints();
app.MapShopEndpoints();

app.Run();

