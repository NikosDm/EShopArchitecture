using Refit;
using Shopping.Web.Models.Basket;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

        public async Task<ShoppingCart> LoadUserBasket()
        {
            // Get Basket If Not Exist Create New Basket with Default Logged In User Name: swn
            var userName = "swn";
            ShoppingCart basket;

            try
            {
                var getBasketResponse = await GetBasket(userName);
                basket = getBasketResponse.Cart;
            }
            catch (Exception)
            {
                basket = new ShoppingCart
                {
                    UserName = userName,
                    Items = []
                };
            }

            return basket;
        }
    }
}