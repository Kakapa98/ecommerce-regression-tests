# Project Notes — Defect Documentation

## Defect #1: Cart Item Count Not Updating in UI After Add-to-Cart

**Date Found:** 2026-09-04
**Severity:** Medium
**Area:** Cart / Products
**Status:** Resolved

### Description

When adding a product to cart from the Products page, the cart badge (`.shopping_cart_badge`) did not consistently appear immediately after clicking the "Add to Cart" button. This caused the `CartItemCount_ShouldUpdateWhenAddingProducts` test to fail intermittently, as it attempted to read the badge count before the UI had updated.

### Steps to Reproduce

1. Log in as `standard_user`
2. Click "Add to Cart" for any product
3. Immediately attempt to read the cart badge count

### Expected Result

The cart badge should appear and display count "1" after adding a product.

### Actual Result

The cart badge was not visible (count returned "0") immediately after adding a product, due to a UI update delay.

### Root Cause

The test assumed the cart badge was synchronously available after the add-to-cart click action completed. In reality, the UI update is asynchronous — the React state update and subsequent DOM re-render happen after the click handler returns.

### Fix Applied

Added an explicit wait for the badge selector before reading the count:

```csharp
await Page!.WaitForSelectorAsync(".shopping_cart_badge");
var badgeCount = await _productsPage.GetCartBadgeCountAsync();
```

This ensures the test waits for the DOM to reflect the cart update before asserting.

### Verification

After the fix, all 28 regression tests pass consistently with `dotnet test --filter "Category=Regression"`.
