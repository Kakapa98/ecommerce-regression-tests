# E-Commerce Regression Testing — Exercise

## Scenario

You are a QA Automation Engineer. Regression testing for an e-commerce app is currently manual, slow, and unreliable — bugs slip through when unrelated changes break existing functionality (e.g. a cart change silently breaks checkout).

Your task: build an automated regression suite that catches this kind of breakage. A CI pipeline is already set up in this repo to build the project and run your tests on every push/PR — your job is to write the tests so that pipeline passes.

## Application Under Test

**SauceDemo** — https://www.saucedemo.com/

```
Username: standard_user
Password: secret_sauce
```

## Your Task

Build a Playwright + C# + NUnit regression suite, following the Page Object Model, covering Login, Products, Cart, and Checkout. Automate it in GitHub Actions so every push runs the suite and reports pass/fail.

## Tech Stack

`C#` · `.NET` · `NUnit` · `Playwright` · `Git` · `GitHub Actions`

## Required Project Structure

```
ecommerce-regression-tests/
├── Tests/
│   ├── LoginTests.cs
│   ├── ProductTests.cs
│   ├── CartTests.cs
│   └── CheckoutTests.cs
├── Pages/
│   ├── LoginPage.cs
│   ├── ProductsPage.cs
│   ├── CartPage.cs
│   └── CheckoutPage.cs
├── TestData/
│   └── TestData.cs
├── Utils/
│   └── TestBase.cs
├── .github/workflows/regression-tests.yml   ← already provided, do not edit
├── README.md
├── PROJECT.md
├── .gitignore
└── RegressionTests.csproj
```

**Rule:** Tests describe *what* is being verified. Page Objects describe *how* the app is interacted with. Keep selectors and browser interaction out of test files.

## Test Coverage (~23 tests)

| # | Area | Test |
|---|------|------|
| 1 | Login | Valid login |
| 2 | Login | Invalid username |
| 3 | Login | Invalid password |
| 4 | Login | Empty credentials |
| 5 | Login | Logout |
| 6 | Products | Products displayed after login |
| 7 | Products | Open product details |
| 8 | Products | Sort by name A–Z / Z–A / price low–high / high–low |
| 9 | Products | Add product to cart |
| 10 | Products | Add multiple products |
| 11 | Products | Product info (name, price) correct |
| 12 | Cart | View cart |
| 13 | Cart | Product appears in cart |
| 14 | Cart | Remove product |
| 15 | Cart | Multiple products in cart |
| 16 | Cart | Cart item count updates |
| 17 | Cart | Continue shopping returns to products |
| 18 | Checkout | Start checkout |
| 19 | Checkout | Valid customer info accepted |
| 20 | Checkout | Missing first name blocked |
| 21 | Checkout | Missing last name blocked |
| 22 | Checkout | Missing postal code blocked |
| 23 | Checkout | Order summary correct |
| 24 | Checkout | Complete order → confirmation shown |

Tag every test with `[Category("Regression")]` plus a functional category (`Login`, `Products`, `Cart`, `Checkout`). Mark the critical-path tests (login, add to cart, checkout, complete order) as your highest priority — these must always pass.

## Steps to Complete

1. **Fork/clone** this repo.
2. **Explore** SauceDemo manually — note the key flows and elements you'll automate.
3. **Scaffold** the C# project; install NUnit and Playwright; confirm one sample test runs.
4. **Build Page Objects** first (`LoginPage`, `ProductsPage`, `CartPage`, `CheckoutPage`).
5. **Automate tests one at a time**, in order: Login → Products → Cart → Checkout. Run each before moving to the next.
6. **Tag tests** with categories so the regression suite can be filtered.
7. **Push your changes** — the pipeline is already configured and will run automatically on push/PR.
8. **Document a defect**: intentionally break something (e.g. cart count logic), watch a test fail, log it as a bug report, fix it, and re-run the suite to confirm it's green again.
9. **Write the README** covering: what the project is, tech used, how to run tests locally, and how CI works.

## Running Tests Locally

```bash
dotnet test
dotnet test --filter "Category=Regression"
```

## Definition of Done

- [ ] All ~23 tests implemented and passing
- [ ] Page Object Model used throughout — no selectors inside test files
- [ ] Tests are categorized and filterable
- [ ] `dotnet test` runs clean locally
- [ ] **GitHub Actions pipeline shows a green check** on your latest commit (pipeline is pre-built — you just need tests that pass)
- [ ] One defect documented (found via a failing test, fixed, retested)
- [ ] README explains setup, coverage, and how to run tests

## How You'll Know You're Done

Push your code, open the **Actions** tab on your GitHub repo, and confirm the `regression-tests` workflow run is green. That's your pass/fail signal — no pipeline run, no green check, not done.