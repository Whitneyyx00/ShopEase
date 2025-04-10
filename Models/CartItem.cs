// Models/CartItem.cs
using ShopEase.Client.Models;

namespace ShopEase.Client.Models
{
    public class CartItem
    {
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}