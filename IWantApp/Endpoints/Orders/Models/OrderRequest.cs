namespace IWantApp.Endpoints.Orders.Models;

public record OrderRequest(List<Guid> ProductIds, string DeliveryAddress);