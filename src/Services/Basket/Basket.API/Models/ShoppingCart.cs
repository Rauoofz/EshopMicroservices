using Marten.Schema;

namespace Basket.API.Models;

public class ShoppingCart
{
    [Identity] public string Username { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);

    public ShoppingCart(string username)
    {
        Username = username;
    }

    public ShoppingCart()
    {
        
    }
}


