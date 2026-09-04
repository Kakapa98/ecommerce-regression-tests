using Microsoft.Playwright;

namespace ecommerce_regression_tests.Pages;

public class CheckoutPage
{
    private readonly IPage _page;

    private readonly ILocator _firstNameInput;
    private readonly ILocator _lastNameInput;
    private readonly ILocator _postalCodeInput;
    private readonly ILocator _continueButton;
    private readonly ILocator _finishButton;
    private readonly ILocator _cancelButton;
    private readonly ILocator _errorMessage;
    private readonly ILocator _summarySubtotal;
    private readonly ILocator _summaryTax;
    private readonly ILocator _summaryTotal;
    private readonly ILocator _completeHeader;
    private readonly ILocator _completeText;
    private readonly ILocator _backHomeButton;
    private readonly ILocator _summaryItems;

    public CheckoutPage(IPage page)
    {
        _page = page;
        _firstNameInput = page.Locator("[data-test=\"firstName\"]");
        _lastNameInput = page.Locator("[data-test=\"lastName\"]");
        _postalCodeInput = page.Locator("[data-test=\"postalCode\"]");
        _continueButton = page.Locator("[data-test=\"continue\"]");
        _finishButton = page.Locator("[data-test=\"finish\"]");
        _cancelButton = page.Locator("[data-test=\"cancel\"]");
        _errorMessage = page.Locator("[data-test=\"error\"]");
        _summarySubtotal = page.Locator(".summary_subtotal_label");
        _summaryTax = page.Locator(".summary_tax_label");
        _summaryTotal = page.Locator(".summary_total_label");
        _completeHeader = page.Locator(".complete-header");
        _completeText = page.Locator(".complete-text");
        _backHomeButton = page.Locator("[data-test=\"back-to-products\"]");
        _summaryItems = page.Locator(".cart_item");
    }

    public async Task FillCheckoutInfoAsync(string firstName, string lastName, string postalCode)
    {
        await _firstNameInput.FillAsync(firstName);
        await _lastNameInput.FillAsync(lastName);
        await _postalCodeInput.FillAsync(postalCode);
    }

    public async Task FillFirstNameAsync(string firstName)
    {
        await _firstNameInput.FillAsync(firstName);
    }

    public async Task FillLastNameAsync(string lastName)
    {
        await _lastNameInput.FillAsync(lastName);
    }

    public async Task FillPostalCodeAsync(string postalCode)
    {
        await _postalCodeInput.FillAsync(postalCode);
    }

    public async Task ClickContinueAsync()
    {
        await _continueButton.ClickAsync();
    }

    public async Task ClickFinishAsync()
    {
        await _finishButton.ClickAsync();
    }

    public async Task ClickCancelAsync()
    {
        await _cancelButton.ClickAsync();
    }

    public async Task<string> GetErrorMessageAsync()
    {
        return await _errorMessage.TextContentAsync() ?? string.Empty;
    }

    public async Task<bool> IsErrorMessageVisibleAsync()
    {
        return await _errorMessage.IsVisibleAsync();
    }

    public async Task<string> GetSummarySubtotalAsync()
    {
        return await _summarySubtotal.TextContentAsync() ?? string.Empty;
    }

    public async Task<string> GetSummaryTaxAsync()
    {
        return await _summaryTax.TextContentAsync() ?? string.Empty;
    }

    public async Task<string> GetSummaryTotalAsync()
    {
        return await _summaryTotal.TextContentAsync() ?? string.Empty;
    }

    public async Task<string> GetCompleteHeaderAsync()
    {
        return await _completeHeader.TextContentAsync() ?? string.Empty;
    }

    public async Task<string> GetCompleteTextAsync()
    {
        return await _completeText.TextContentAsync() ?? string.Empty;
    }

    public async Task ClickBackHomeAsync()
    {
        await _backHomeButton.ClickAsync();
    }

    public async Task<bool> IsCompleteHeaderVisibleAsync()
    {
        return await _completeHeader.IsVisibleAsync();
    }

    public async Task<int> GetSummaryItemCountAsync()
    {
        return await _summaryItems.CountAsync();
    }

    public Task<string> GetCurrentUrlAsync()
    {
        return Task.FromResult(_page.Url);
    }
}
