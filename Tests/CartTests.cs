using ecommerce_regression_tests.Pages;
using ecommerce_regression_tests.TestData;
using ecommerce_regression_tests.Utils;

namespace ecommerce_regression_tests.Tests;

[TestFixture]
[Category("Regression")]
[Category("Cart")]
public class CartTests : TestBase
{
    private ProductsPage _productsPage = null!;
    private CartPage _cartPage = null!;

    [SetUp]
    public async Task SetUp()
    {
        var loginPage = new LoginPage(Page!);
        await loginPage.NavigateAsync(TestConstants.BaseUrl);
        await loginPage.LoginAsync(TestConstants.ValidUsername, TestConstants.ValidPassword);
        _productsPage = new ProductsPage(Page!);
        _cartPage = new CartPage(Page!);
    }

    [Test]
    [Category("Critical")]
    public async Task ViewCart_ShouldShowCartPage()
    {
        await _productsPage.ClickCartAsync();
        var url = await _cartPage.GetCurrentUrlAsync();
        Assert.That(url, Does.Contain(TestConstants.CartPageUrl));
    }

    [Test]
    [Category("Critical")]
    public async Task ProductAppearsInCart_ShouldShowAddedProduct()
    {
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBackpack);
        await _productsPage.ClickCartAsync();
        var isInCart = await _cartPage.IsProductInCartAsync("Sauce Labs Backpack");
        Assert.That(isInCart, Is.True);
    }

    [Test]
    [Category("Critical")]
    public async Task RemoveProduct_ShouldRemoveFromCart()
    {
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBackpack);
        await _productsPage.ClickCartAsync();
        await _cartPage.RemoveProductAsync(TestConstants.ProductBackpack);
        var count = await _cartPage.GetCartItemCountAsync();
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    [Category("Critical")]
    public async Task MultipleProductsInCart_ShouldShowAllProducts()
    {
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBackpack);
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBikeLight);
        await _productsPage.ClickCartAsync();
        var count = await _cartPage.GetCartItemCountAsync();
        Assert.That(count, Is.EqualTo(2));
    }

    [Test]
    public async Task CartItemCount_ShouldUpdateWhenAddingProducts()
    {
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBackpack);
        await Page!.WaitForSelectorAsync(".shopping_cart_badge");
        var badgeCount = await _productsPage.GetCartBadgeCountAsync();
        await _productsPage.ClickCartAsync();
        var cartCount = await _cartPage.GetCartItemCountAsync();
        Assert.That(badgeCount, Is.EqualTo("1"));
        Assert.That(cartCount, Is.EqualTo(1));
    }

    [Test]
    public async Task ContinueShopping_ShouldReturnToProductsPage()
    {
        await _productsPage.ClickCartAsync();
        await _cartPage.ClickContinueShoppingAsync();
        var url = await _productsPage.GetCurrentUrlAsync();
        Assert.That(url, Does.Contain(TestConstants.ProductsPageUrl));
    }
}
