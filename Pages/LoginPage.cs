using Microsoft.Playwright;

namespace ecommerce_regression_tests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    private readonly ILocator _usernameInput;
    private readonly ILocator _passwordInput;
    private readonly ILocator _loginButton;
    private readonly ILocator _errorMessage;

    public LoginPage(IPage page)
    {
        _page = page;
        _usernameInput = page.Locator("#user-name");
        _passwordInput = page.Locator("#password");
        _loginButton = page.Locator("#login-button");
        _errorMessage = page.Locator("[data-test=\"error\"]");
    }

    public async Task NavigateAsync(string baseUrl)
    {
        await _page.GotoAsync(baseUrl);
    }

    public async Task LoginAsync(string username, string password)
    {
        await _usernameInput.FillAsync(username);
        await _passwordInput.FillAsync(password);
        await _loginButton.ClickAsync();
    }

    public async Task LoginWithUsernameOnlyAsync(string username)
    {
        await _usernameInput.FillAsync(username);
        await _loginButton.ClickAsync();
    }

    public async Task LoginWithPasswordOnlyAsync(string password)
    {
        await _passwordInput.FillAsync(password);
        await _loginButton.ClickAsync();
    }

    public async Task LoginWithEmptyCredentialsAsync()
    {
        await _loginButton.ClickAsync();
    }

    public async Task<string> GetErrorMessageAsync()
    {
        return await _errorMessage.TextContentAsync() ?? string.Empty;
    }

    public async Task<bool> IsErrorMessageVisibleAsync()
    {
        return await _errorMessage.IsVisibleAsync();
    }

    public Task<string> GetCurrentUrlAsync()
    {
        return Task.FromResult(_page.Url);
    }
}
