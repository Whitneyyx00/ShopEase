// Models/Order.cs
using System;
using System.Collections.Generic;

namespace ShopEase.Client.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public string? Status { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}