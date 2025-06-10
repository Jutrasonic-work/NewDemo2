namespace FrontEnd.Api.Configuration;

public class ApiSettings
{
    public required CatalogApiSettings CatalogApi { get; set; }
    public required OrderingApiSettings OrderingApi { get; set; }
    public required AuthApiSettings AuthApi { get; set; }
}

public class CatalogApiSettings
{
    public required string BaseUrl { get; set; }
}

public class OrderingApiSettings
{
    public required string BaseUrl { get; set; }
}

public class AuthApiSettings
{
    public required string BaseUrl { get; set; }
} 