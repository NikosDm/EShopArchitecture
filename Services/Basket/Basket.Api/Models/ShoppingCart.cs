namespace Basket.Api.Models
{
    public class ShoppingCart
    {
        public string Username { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = [];
        public decimal Price => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string Username)
        {
            this.Username = Username;
        }

        // Required for mapping
        public ShoppingCart()
        {
            
        }
    }
}