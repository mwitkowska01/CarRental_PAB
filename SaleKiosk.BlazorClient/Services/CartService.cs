using Blazored.LocalStorage;
using CarRental.BlazorClient.ViewModels;

namespace CarRental.BlazorClient.Services
{
    public interface ICartService
    {
        Task Increment(CartItem cartItem);
        Task Decrement(CartItem cartItem);
        Task<List<CartItem>> GetCart();
        Task DeleteCart();

    }

    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorageService;

        public CartService(ILocalStorageService localStorageService)
        {
            this._localStorageService = localStorageService;
        }

        public async Task<List<CartItem>> GetCart()
        {
            // read or create new cart
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("SaleKioskCart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            return cart;
        }

        public async Task Increment(CartItem cartItem)
        {
            // cart item alredy exists in local storage
            bool alreadyExists = false;

            // read or create new cart
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("SaleKioskCart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            // update count in existing cart item
            foreach (var ci in cart)
            {
                if (ci.Service.Id == cartItem.Service.Id)
                {
                    alreadyExists = true;
                    ci.Count += cartItem.Count;
                }
            }

            // create new cart item if does not exist
            if (!alreadyExists)
            {
                cart.Add(cartItem);
            }

            // update local storage
            await _localStorageService.SetItemAsync("SaleKioskCart", cart);
        }

        public async Task Decrement(CartItem cartItem)
        {
            CartItem toDelete = null;

            // read or create new cart
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("SaleKioskCart");
            if (cart == null)
                return;

            // update count in existing cart item
            foreach (var ci in cart)
            {
                if (ci.Service.Id == cartItem.Service.Id)
                {
                    ci.Count -= cartItem.Count;
                    if (ci.Count <= 0)
                    {
                        toDelete = ci;
                    }
                }
            }

            // remove cart item
            if (toDelete != null)
                cart.Remove(toDelete);

            // update local storage
            await _localStorageService.SetItemAsync("SaleKioskCart", cart);
        }

        public async Task DeleteCart()
        {
            await _localStorageService.ClearAsync();
        }
    }
}
