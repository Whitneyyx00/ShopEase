// Models/Cart.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopEase.Api.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        [NotMapped]
        public decimal TotalPrice { get; set; }
    }

    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CartId { get; set; }
        public Product? Product { get; set; }
        public Cart? Cart { get; set; }
    }
}