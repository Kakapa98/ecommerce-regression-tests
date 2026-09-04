using ecommerce_regression_tests.Pages;
using ecommerce_regression_tests.TestData;
using ecommerce_regression_tests.Utils;

namespace ecommerce_regression_tests.Tests;

[TestFixture]
[Category("Regression")]
[Category("Checkout")]
public class CheckoutTests : TestBase
{
    private ProductsPage _productsPage = null!;
    private CartPage _cartPage = null!;
    private CheckoutPage _checkoutPage = null!;

    [SetUp]
    public async Task SetUp()
    {
        var loginPage = new LoginPage(Page!);
        await loginPage.NavigateAsync(TestConstants.BaseUrl);
        await loginPage.LoginAsync(TestConstants.ValidUsername, TestConstants.ValidPassword);
        _productsPage = new ProductsPage(Page!);
        _cartPage = new CartPage(Page!);
        _checkoutPage = new CheckoutPage(Page!);
    }

    private async Task AddProductAndGoToCheckout()
    {
        await _productsPage.AddProductToCartAsync(TestConstants.ProductBackpack);
        await _productsPage.ClickCartAsync();
        await _cartPage.ClickCheckoutAsync();
    }

    [Test]
    [Category("Critical")]
    public async Task StartCheckout_ShouldNavigateToCheckoutPage()
    {
        await AddProductAndGoToCheckout();
        var url = await _checkoutPage.GetCurrentUrlAsync();
        Assert.That(url, Does.Contain(TestConstants.CheckoutStepOneUrl));
    }

    [Test]
    [Category("Critical")]
    public async Task ValidCustomerInfo_ShouldAcceptAndContinue()
    {
        await AddProductAndGoToCheckout();
        await _checkoutPage.FillCheckoutInfoAsync(TestConstants.FirstName, TestConstants.LastName, TestConstants.PostalCode);
        await _checkoutPage.ClickContinueAsync();
        var url = await _checkoutPage.GetCurrentUrlAsync();
        Assert.That(url, Does.Contain(TestConstants.CheckoutStepTwoUrl));
    }

    [Test]
    public async Task MissingFirstName_ShouldShowError()
    {
        await AddProductAndGoToCheckout();
        await _checkoutPage.FillCheckoutInfoAsync(string.Empty, TestConstants.LastName, TestConstants.PostalCode);
        await _checkoutPage.ClickContinueAsync();
        var isVisible = await _checkoutPage.IsErrorMessageVisibleAsync();
        Assert.That(isVisible, Is.True);
    }

    [Test]
    public async Task MissingLastName_ShouldShowError()
    {
        await AddProductAndGoToCheckout();
        await _checkoutPage.FillCheckoutInfoAsync(TestConstants.FirstName, string.Empty, TestConstants.PostalCode);
        await _checkoutPage.ClickContinueAsync();
        var isVisible = await _checkoutPage.IsErrorMessageVisibleAsync();
        Assert.That(isVisible, Is.True);
    }

    [Test]
    public async Task MissingPostalCode_ShouldShowError()
    {
        await AddProductAndGoToCheckout();
        await _checkoutPage.FillCheckoutInfoAsync(TestConstants.FirstName, TestConstants.LastName, string.Empty);
        await _checkoutPage.ClickContinueAsync();
        var isVisible = await _checkoutPage.IsErrorMessageVisibleAsync();
        Assert.That(isVisible, Is.True);
    }

    [Test]
    [Category("Critical")]
    public async Task OrderSummary_ShouldShowCorrectItemAndTotal()
    {
        await AddProductAndGoToCheckout();
        await _checkoutPage.FillCheckoutInfoAsync(TestConstants.FirstName, TestConstants.LastName, TestConstants.PostalCode);
        await _checkoutPage.ClickContinueAsync();
        var itemCount = await _checkoutPage.GetSummaryItemCountAsync();
        var totalText = await _checkoutPage.GetSummaryTotalAsync();
        Assert.That(itemCount, Is.EqualTo(1));
        Assert.That(totalText, Does.Contain("$"));
    }

    [Test]
    [Category("Critical")]
    public async Task CompleteOrder_ShouldShowConfirmation()
    {
        await AddProductAndGoToCheckout();
        await _checkoutPage.FillCheckoutInfoAsync(TestConstants.FirstName, TestConstants.LastName, TestConstants.PostalCode);
        await _checkoutPage.ClickContinueAsync();
        await _checkoutPage.ClickFinishAsync();
        var header = await _checkoutPage.GetCompleteHeaderAsync();
        var isVisible = await _checkoutPage.IsCompleteHeaderVisibleAsync();
        Assert.That(isVisible, Is.True);
        Assert.That(header, Does.Contain("Thank you for your order"));
    }
}
