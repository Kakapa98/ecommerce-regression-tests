using Microsoft.Playwright;

namespace ecommerce_regression_tests.Pages;

public class ProductsPage
{
    private readonly IPage _page;

    private readonly ILocator _productList;
    private readonly ILocator _productItems;
    private readonly ILocator _productNames;
    private readonly ILocator _productPrices;
    private readonly ILocator _sortDropdown;
    private readonly ILocator _cartBadge;
    private readonly ILocator _cartLink;
    private readonly ILocator _burgerMenu;
    private readonly ILocator _logoutLink;

    public ProductsPage(IPage page)
    {
        _page = page;
        _productList = page.Locator(".inventory_list");
        _productItems = page.Locator(".inventory_item");
        _productNames = page.Locator(".inventory_item_name");
        _productPrices = page.Locator(".inventory_item_price");
        _sortDropdown = page.Locator("[data-test=\"product-sort-container\"]");
        _cartBadge = page.Locator(".shopping_cart_badge");
        _cartLink = page.Locator(".shopping_cart_link");
        _burgerMenu = page.Locator("#react-burger-menu-btn");
        _logoutLink = page.Locator("[data-test=\"logout-sidebar-link\"]");
    }

    public async Task<bool> IsProductListVisibleAsync()
    {
        return await _productList.IsVisibleAsync();
    }

    public async Task<int> GetProductCountAsync()
    {
        return await _productItems.CountAsync();
    }

    public async Task<List<string>> GetProductNamesAsync()
    {
        var names = new List<string>();
        var count = await _productNames.CountAsync();
        for (int i = 0; i < count; i++)
        {
            names.Add(await _productNames.Nth(i).TextContentAsync() ?? string.Empty);
        }
        return names;
    }

    public async Task<List<string>> GetProductPricesAsync()
    {
        var prices = new List<string>();
        var count = await _productPrices.CountAsync();
        for (int i = 0; i < count; i++)
        {
            prices.Add(await _productPrices.Nth(i).TextContentAsync() ?? string.Empty);
        }
        return prices;
    }

    public async Task SortProductsAsync(string sortOption)
    {
        await _sortDropdown.SelectOptionAsync(sortOption);
    }

    public async Task AddProductToCartAsync(string productSuffix)
    {
        var addButton = _page.Locator($"[data-test=\"add-to-cart-{productSuffix}\"]");
        await addButton.ClickAsync();
    }

    public async Task RemoveProductFromCartAsync(string productSuffix)
    {
        var removeButton = _page.Locator($"[data-test=\"remove-{productSuffix}\"]");
        await removeButton.ClickAsync();
    }

    public async Task<bool> IsAddButtonVisibleAsync(string productSuffix)
    {
        var addButton = _page.Locator($"[data-test=\"add-to-cart-{productSuffix}\"]");
        return await addButton.IsVisibleAsync();
    }

    public async Task<bool> IsRemoveButtonVisibleAsync(string productSuffix)
    {
        var removeButton = _page.Locator($"[data-test=\"remove-{productSuffix}\"]");
        return await removeButton.IsVisibleAsync();
    }

    public async Task<string> GetCartBadgeCountAsync()
    {
        return await _cartBadge.TextContentAsync() ?? "0";
    }

    public async Task<bool> IsCartBadgeVisibleAsync()
    {
        return await _cartBadge.IsVisibleAsync();
    }

    public async Task ClickCartAsync()
    {
        await _cartLink.ClickAsync();
    }

    public async Task OpenProductDetailsAsync(string productSuffix)
    {
        var productName = _page.Locator($"[data-test=\"inventory-item-name-{productSuffix}\"]");
        await productName.ClickAsync();
    }

    public async Task OpenProductDetailsByNameAsync(string name)
    {
        await _page.Locator(".inventory_item_name", new PageLocatorOptions { HasText = name }).ClickAsync();
    }

    public async Task ClickLogoutAsync()
    {
        await _burgerMenu.ClickAsync();
        await _logoutLink.ClickAsync();
    }

    public Task<string> GetCurrentUrlAsync()
    {
        return Task.FromResult(_page.Url);
    }

    public async Task<string> GetProductPriceAsync(string productSuffix)
    {
        var priceLocator = _page.Locator($"[data-test=\"inventory-item-price-{productSuffix}\"]");
        return await priceLocator.TextContentAsync() ?? string.Empty;
    }

    public async Task<string> GetProductNameAsync(string productSuffix)
    {
        var nameLocator = _page.Locator($"[data-test=\"inventory-item-name-{productSuffix}\"]");
        return await nameLocator.TextContentAsync() ?? string.Empty;
    }
}
