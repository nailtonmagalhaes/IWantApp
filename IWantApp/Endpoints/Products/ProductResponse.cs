namespace IWantApp.Endpoints.Products;

public record ProductResponse(string Name, string Category, string Description, bool HasStock, decimal Price, bool Active);