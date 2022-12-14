﻿namespace IWantApp.Domain.Orders;

public class Order : Entity
{
    public string ClientId { get; private set; }
    public List<Product> Products { get; private set; }
    public decimal Total { get; private set; }
    public string DeliveryAddress { get; private set; }

    private Order() { }

    public Order(string clientId, string clientName, List<Product> products, string deliveryAddress)
    {
        ClientId = clientId;
        Products = products;
        DeliveryAddress = deliveryAddress;
        CreatedBy = clientName;
        EditedBy = clientName;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Total = products?.Sum(x => x.Price) ?? 0;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNull(ClientId, "Client")
            .IsTrue(Products != null && Products.Any(), nameof(Products), "Products should be informed!")
            .IsNotNull(DeliveryAddress, nameof(DeliveryAddress));
        AddNotifications(contract);
    }
}
