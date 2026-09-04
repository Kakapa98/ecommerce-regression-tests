using ecommerce_regression_tests.Pages;
using ecommerce_regression_tests.TestData;
using ecommerce_regression_tests.Utils;

namespace ecommerce_regression_tests.Tests;

[TestFixture]
[Category("Regression")]
[Category("Products")]
public class ProductTests : TestBase
{
    private ProductsPage _productsPage = null!;

    [SetUp]
    public async Task SetUp()
    {
        var loginPage = new LoginPage(Page!);
        await loginPage.NavigateAsync(TestConstants.BaseUrl);
        await loginPage.LoginAsync(TestConstants.ValidUsername, TestConstants.ValidPassword);
        _productsPage = new ProductsPage(Page!);
    }

    [Test]
    public async Task ProductsDisplayed_ShouldShowProductList()
    {
        var isVisible = await _productsPage.IsProductListVisibleAsync();
        Assert.That(isVisible, Is.True);
    }

    [Test]
    public async Task ProductsDisplayed_ShouldHaveSixProducts()
    {
        var count = await _productsPage.GetProductCountAsync();
        Assert.That(count, Is.EqualTo(6));
    }

    [Test]
    public async Task OpenProductDetails_ShouldNavigateToProductPage()
    {
        await _productsPage.OpenProductDetailsByNameAsync("Sauce Labs Backpack");
        var url = await _productsPage.GetCurrentUrlAsync();
        Assert.That(url, Does.Contain("/inventory-item.html"));
    }

    [Test]
    public async Task SortByNameAZ_ShouldSortAlphabeticallyAscending()
    {
        await _productsPage.SortProductsAsync("az");
        var names = await _productsPage.GetProductNamesAsync();
        var sortedNames = names.OrderBy(n => n).ToList();
        Assert.That(names, Is.EqualTo(sortedNames));
    }

    [Test]
    public async Task SortByNameZA_ShouldSortAlphabeticallyDescending()
    {
        await _productsPage.SortProductsAsync("za");
        var names = await _productsPage.GetProductNamesAsync();
        var sortedNames = names.OrderByDescending(n => n).ToList();
        Assert.That(names, Is.EqualTo(sortedNames));
    }

    [Test]
    public async Task SortByPriceLowHigh_ShouldSortPriceAscending()
    {
        await _productsPage.SortProductsAsync("lohi");
        var prices = await _productsPage.GetProductPricesAsync();
        var parsedPrices = prices.Select(p => decimal.Parse(p.Replace("$", ""))).ToList();
        var sortedPrices = parsedPrices.OrderBy(p => p).ToList();
        Assert.That(parsedPrices, Is.EqualTo(sortedPrices));
    }

    [Test]
    public async Task SortByPriceHighLow_ShouldSortPriceDescending()
    {
        await _productsPage.SortProductsAsync("hilo");
        var prices = await _productsPage.GetProductPricesAsync();
        var parsedPrices = prices.Select(p => decimal.Parse(p.Replace("$", ""))).ToList();
        var sortedPrices = parsedPrices.OrderByDescending(p => p).ToList();
        Assert.That(parsedPrices, Is.EqualTo(sortedPrices));
    }

    [Test]
    [Category("Critical")]
    public async Task AddProductToCart_ShouldUpdateCartBadge()
    {
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBackpack);
        var badgeVisible = await _productsPage.IsCartBadgeVisibleAsync();
        var badgeCount = await _productsPage.GetCartBadgeCountAsync();
        Assert.That(badgeVisible, Is.True);
        Assert.That(badgeCount, Is.EqualTo("1"));
    }

    [Test]
    [Category("Critical")]
    public async Task AddMultipleProducts_ShouldShowCorrectCount()
    {
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBackpack);
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBikeLight);
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBoltShirt);
        var badgeCount = await _productsPage.GetCartBadgeCountAsync();
        Assert.That(badgeCount, Is.EqualTo("3"));
    }

    [Test]
    public async Task ProductInfo_ShouldHaveNameAndPrice()
    {
        var names = await _productsPage.GetProductNamesAsync();
        var prices = await _productsPage.GetProductPricesAsync();
        Assert.That(names.Count, Is.GreaterThan(0));
        Assert.That(prices.Count, Is.GreaterThan(0));
        Assert.That(names.All(n => !string.IsNullOrWhiteSpace(n)), Is.True);
        Assert.That(prices.All(p => p.StartsWith("$")), Is.True);
    }
}
