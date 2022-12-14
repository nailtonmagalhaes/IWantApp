namespace IWantApp.Endpoints.Products.Models;

public record ProductRequest(string Name, Guid CategoryId, string Description, bool HasStock, decimal Price, bool Active);