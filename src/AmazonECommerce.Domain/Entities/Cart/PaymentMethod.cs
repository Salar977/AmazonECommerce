﻿namespace AmazonECommerce.Domain.Entities.Cart;

public class PaymentMethod
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
}