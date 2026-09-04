# E-Commerce Regression Test Suite

Automated regression tests for [SauceDemo](https://www.saucedemo.com/) using Playwright, C#, and NUnit.

## What This Project Does

This is an automated regression testing suite that verifies core e-commerce flows on SauceDemo — login, product browsing, cart management, and checkout — to catch breakage from unrelated changes.

## Tech Stack

- **C# / .NET 8.0**
- **NUnit** — test framework
- **Playwright** — browser automation
- **Page Object Model** — test architecture

## Project Structure

```
ecommerce-regression-tests/
├── Tests/
│   ├── LoginTests.cs         # 5 login tests
│   ├── ProductTests.cs       # 10 product tests
│   ├── CartTests.cs          # 6 cart tests
│   └── CheckoutTests.cs      # 7 checkout tests
├── Pages/
│   ├── LoginPage.cs          # Login page interactions
│   ├── ProductsPage.cs       # Products page interactions
│   ├── CartPage.cs           # Cart page interactions
│   └── CheckoutPage.cs       # Checkout page interactions
├── TestData/
│   └── TestData.cs           # Test constants (credentials, URLs, product IDs)
├── Utils/
│   └── TestBase.cs           # Browser setup/teardown base class
├── .github/workflows/
│   └── regression-tests.yml  # CI pipeline (pre-configured)
├── README.md
└── RegressionTests.csproj
```

## Test Coverage (28 tests)

| Area | Tests | Critical Path |
|------|-------|---------------|
| Login | 5 | Valid login, invalid credentials, empty fields, logout |
| Products | 10 | Display, details, sort (4 variants), add to cart, product info |
| Cart | 6 | View, add/remove, multiple items, continue shopping |
| Checkout | 7 | Start, valid info, missing fields (3), order summary, complete |

## How to Run Locally

### Prerequisites

- .NET 8.0 SDK
- Playwright browsers installed

### Install Playwright browsers

```bash
dotnet build
pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps chromium
```

Or using the Playwright CLI:
```bash
dotnet tool install --global Microsoft.Playwright.CLI
playwright install chromium
```

### Run tests

```bash
# Run all regression tests
dotnet test --filter "Category=Regression"

# Run by area
dotnet test --filter "Category=Login"
dotnet test --filter "Category=Products"
dotnet test --filter "Category=Cart"
dotnet test --filter "Category=Checkout"

# Run critical-path tests only
dotnet test --filter "Category=Critical"

# Run all tests
dotnet test
```

## CI/CD

The GitHub Actions pipeline (`.github/workflows/regression-tests.yml`) runs automatically on every push/PR to `main`:

1. Restores dependencies
2. Builds the project
3. Installs Playwright Chromium browser
4. Runs all tests tagged with `Category=Regression`

## Architecture

Tests follow the **Page Object Model** pattern:
- **Page Objects** (`Pages/`) encapsulate all browser selectors and interactions
- **Tests** (`Tests/`) describe *what* is being verified, not *how*
- **TestData** (`TestData/`) centralizes credentials, URLs, and product identifiers
- **TestBase** (`Utils/`) provides browser lifecycle management with screenshot-on-failure

## Test Categories

Every test is tagged with:
- `[Category("Regression")]` — included in the full regression suite
- `[Category("Login|Products|Cart|Checkout")]` — functional area filter
- `[Category("Critical")]` — critical-path tests that must always pass

## Defect Documentation

See [PROJECT.md](PROJECT.md) for the documented defect found and fixed during development.
