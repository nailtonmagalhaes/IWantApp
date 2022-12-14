namespace IWantApp.Endpoints.Orders.Models;

public record OrderResponse(Guid Id, string ClientEmail, IEnumerable<OrderProductResponse> Products, string DeliveryAddress);
