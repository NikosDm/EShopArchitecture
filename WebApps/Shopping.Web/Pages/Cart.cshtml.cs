using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    public class Cart(IBasketService basketService, ILogger<Cart> logger)
        : PageModel
    {
        public ShoppingCart CartModel { get; set; } = new ShoppingCart();

        public async Task<IActionResult> OnGetAsync()
        {
            CartModel = await basketService.LoadUserBasket();
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
        {
            logger.LogInformation("Remove to cart button clicked");
            CartModel = await basketService.LoadUserBasket();
            CartModel.Items.RemoveAll(x => x.ProductId == productId);
            await basketService.StoreBasket(new StoreBasketRequest(CartModel));
            return RedirectToPage();
        }
    }
}