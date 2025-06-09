using FrontEnd.Api.Features.Account;
using FrontEnd.Api.Features.Account.Endpoints;
using FrontEnd.Api.Features.Admin;
using FrontEnd.Api.Features.Admin.Endpoints;
using FrontEnd.Api.Features.Shop;
using FrontEnd.Api.Features.Shop.Endpoints;
using FrontEnd.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder => builder
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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

app.UseCors("AllowVueApp");
app.UseHttpsRedirection();

// Map endpoints
app.MapAccountEndpoints();
app.MapAdminEndpoints();
app.MapShopEndpoints();

app.Run();

