using Microsoft.Playwright;

namespace ecommerce_regression_tests.Pages;

public class CartPage
{
    private readonly IPage _page;

    private readonly ILocator _cartList;
    private readonly ILocator _cartItems;
    private readonly ILocator _cartItemNames;
    private readonly ILocator _cartItemPrices;
    private readonly ILocator _cartItemQuantities;
    private readonly ILocator _removeButtons;
    private readonly ILocator _checkoutButton;
    private readonly ILocator _continueShoppingButton;

    public CartPage(IPage page)
    {
        _page = page;
        _cartList = page.Locator(".cart_list");
        _cartItems = page.Locator(".cart_item");
        _cartItemNames = page.Locator(".inventory_item_name");
        _cartItemPrices = page.Locator(".inventory_item_price");
        _cartItemQuantities = page.Locator(".cart_quantity");
        _removeButtons = page.Locator(".cart_button");
        _checkoutButton = page.Locator("[data-test=\"checkout\"]");
        _continueShoppingButton = page.Locator("[data-test=\"continue-shopping\"]");
    }

    public async Task<bool> IsCartVisibleAsync()
    {
        return await _cartList.IsVisibleAsync();
    }

    public async Task<int> GetCartItemCountAsync()
    {
        return await _cartItems.CountAsync();
    }

    public async Task<List<string>> GetCartItemNamesAsync()
    {
        var names = new List<string>();
        var count = await _cartItemNames.CountAsync();
        for (int i = 0; i < count; i++)
        {
            names.Add(await _cartItemNames.Nth(i).TextContentAsync() ?? string.Empty);
        }
        return names;
    }

    public async Task<List<string>> GetCartItemPricesAsync()
    {
        var prices = new List<string>();
        var count = await _cartItemPrices.CountAsync();
        for (int i = 0; i < count; i++)
        {
            prices.Add(await _cartItemPrices.Nth(i).TextContentAsync() ?? string.Empty);
        }
        return prices;
    }

    public async Task<int> GetCartItemQuantityAsync(int index)
    {
        var text = await _cartItemQuantities.Nth(index).TextContentAsync() ?? "0";
        return int.Parse(text);
    }

    public async Task RemoveProductAsync(string productSuffix)
    {
        var removeButton = _page.Locator($"[data-test=\"remove-{productSuffix}\"]");
        await removeButton.ClickAsync();
    }

    public async Task<bool> IsProductInCartAsync(string productName)
    {
        var items = await GetCartItemNamesAsync();
        return items.Any(name => name.Contains(productName, StringComparison.OrdinalIgnoreCase));
    }

    public async Task ClickCheckoutAsync()
    {
        await _checkoutButton.ClickAsync();
    }

    public async Task ClickContinueShoppingAsync()
    {
        await _continueShoppingButton.ClickAsync();
    }

    public Task<string> GetCurrentUrlAsync()
    {
        return Task.FromResult(_page.Url);
    }
}
