using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SalesItemsITests
{
    /// <summary>
    /// Tests that the total is calculated correctly for a valid SaleItem.
    /// </summary>
    [Fact(DisplayName = "Item total should calculate correctly")]
    public void Given_ValidSaleItem_When_CalculatingTotal_Then_ShouldReturnCorrectValue()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductName = "Product A",
            Quantity = 5,
            UnitPrice = 10.00m,
            Discount = 5.00m
        };

        // Act
        var total = saleItem.ItemTotal;

        // Assert
        Assert.Equal(45.00m, total); // (5 * 10) - 5
    }

    /// <summary>
    /// Tests that validation passes for a valid SaleItem.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for a valid SaleItem")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductName = "Product A",
            Quantity = 5,
            UnitPrice = 10.00m,
            Discount = 5.00m
        };

        // Act
        var isValid = saleItem.IsValid();

        // Assert
        Assert.True(isValid);
    }

    /// <summary>
    /// Tests that validation fails for an invalid SaleItem.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for an invalid SaleItem")]
    public void Given_InvalidSaleItem_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductName = "", // Invalid: empty
            Quantity = 25,    // Invalid: exceeds max allowed
            UnitPrice = 0.00m, // Invalid: must be greater than 0
            Discount = -5.00m  // Invalid: cannot be negative
        };

        // Act
        var isValid = saleItem.IsValid();

        // Assert
        Assert.False(isValid);
    }

    /// <summary>
    /// Tests that a sale item with no discount calculates the correct total.
    /// </summary>
    [Fact(DisplayName = "Item total should calculate correctly with no discount")]
    public void Given_ValidSaleItemWithNoDiscount_When_CalculatingTotal_Then_ShouldReturnCorrectValue()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductName = "Product B",
            Quantity = 3,
            UnitPrice = 15.00m,
            Discount = 0.00m
        };

        // Act
        var total = saleItem.ItemTotal;

        // Assert
        Assert.Equal(45.00m, total); // (3 * 15) - 0
    }

    /// <summary>
    /// Tests that a sale item with maximum allowed quantity passes validation.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for maximum allowed quantity")]
    public void Given_SaleItemWithMaxQuantity_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductName = "Product C",
            Quantity = 20, // Maximum allowed
            UnitPrice = 5.00m,
            Discount = 10.00m
        };

        // Act
        var isValid = saleItem.IsValid();

        // Assert
        Assert.True(isValid);
    }

    /// <summary>
    /// Tests that a sale item with quantity exceeding the limit fails validation.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for quantity exceeding the limit")]
    public void Given_SaleItemWithExceedingQuantity_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductName = "Product D",
            Quantity = 21, // Exceeds max allowed
            UnitPrice = 5.00m,
            Discount = 2.00m
        };

        // Act
        var isValid = saleItem.IsValid();

        // Assert
        Assert.False(isValid);
    }

}