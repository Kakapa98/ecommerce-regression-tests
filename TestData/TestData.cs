namespace ecommerce_regression_tests.TestData;

public static class TestConstants
{
    public const string BaseUrl = "https://www.saucedemo.com";

    public const string ValidUsername = "standard_user";
    public const string ValidPassword = "secret_sauce";
    public const string InvalidUsername = "invalid_user";
    public const string InvalidPassword = "wrong_password";

    public const string FirstName = "Mpho";
    public const string LastName = "Mofokeng";
    public const string PostalCode = "12345";

    public const string ProductBackpack = "sauce-labs-backpack";
    public const string ProductBikeLight = "sauce-labs-bike-light";
    public const string ProductBoltShirt = "sauce-labs-bolt-t-shirt";
    public const string ProductFleeceJacket = "sauce-labs-fleece-jacket";
    public const string ProductOnesie = "sauce-labs-onesie";
    public const string ProductRedTShirt = "test.allthethings()-t-shirt-(red)";

    public static readonly string[] AllProducts = new[]
    {
        ProductBackpack,
        ProductBikeLight,
        ProductBoltShirt,
        ProductFleeceJacket,
        ProductOnesie,
        ProductRedTShirt
    };

    public const string LoginPageUrl = "/";
    public const string ProductsPageUrl = "/inventory.html";
    public const string CartPageUrl = "/cart.html";
    public const string CheckoutStepOneUrl = "/checkout-step-one.html";
    public const string CheckoutStepTwoUrl = "/checkout-step-two.html";
    public const string CheckoutCompleteUrl = "/checkout-complete.html";
}
