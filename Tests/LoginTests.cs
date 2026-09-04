using ecommerce_regression_tests.Pages;
using ecommerce_regression_tests.TestData;
using ecommerce_regression_tests.Utils;

namespace ecommerce_regression_tests.Tests;

[TestFixture]
[Category("Regression")]
[Category("Login")]
public class LoginTests : TestBase
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public async Task SetUp()
    {
        _loginPage = new LoginPage(Page!);
        await _loginPage.NavigateAsync(TestConstants.BaseUrl);
    }

    [Test]
    [Category("Critical")]
    public async Task ValidLogin_ShouldRedirectToProductsPage()
    {
        await _loginPage.LoginAsync(TestConstants.ValidUsername, TestConstants.ValidPassword);
        var url = await _loginPage.GetCurrentUrlAsync();
        Assert.That(url, Does.Contain(TestConstants.ProductsPageUrl));
    }

    [Test]
    [Category("Critical")]
    public async Task InvalidUsername_ShouldShowErrorMessage()
    {
        await _loginPage.LoginAsync(TestConstants.InvalidUsername, TestConstants.ValidPassword);
        var isVisible = await _loginPage.IsErrorMessageVisibleAsync();
        Assert.That(isVisible, Is.True);
    }

    [Test]
    [Category("Critical")]
    public async Task InvalidPassword_ShouldShowErrorMessage()
    {
        await _loginPage.LoginAsync(TestConstants.ValidUsername, TestConstants.InvalidPassword);
        var isVisible = await _loginPage.IsErrorMessageVisibleAsync();
        Assert.That(isVisible, Is.True);
    }

    [Test]
    public async Task EmptyCredentials_ShouldShowErrorMessage()
    {
        await _loginPage.LoginWithEmptyCredentialsAsync();
        var isVisible = await _loginPage.IsErrorMessageVisibleAsync();
        Assert.That(isVisible, Is.True);
    }

    [Test]
    [Category("Critical")]
    public async Task Logout_ShouldReturnToLoginPage()
    {
        await _loginPage.LoginAsync(TestConstants.ValidUsername, TestConstants.ValidPassword);
        var productsPage = new ProductsPage(Page!);
        await productsPage.ClickLogoutAsync();
        var url = await _loginPage.GetCurrentUrlAsync();
        Assert.That(url, Does.Contain(TestConstants.LoginPageUrl));
    }
}
