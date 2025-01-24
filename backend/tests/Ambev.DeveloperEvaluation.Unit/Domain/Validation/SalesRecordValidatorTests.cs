using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SalesRecordValidator class.
/// Tests cover validation of all sales record properties, including sale date, customer name,
/// total sale amount, branch, and sale items.
/// </summary>
public class SalesRecordValidatorTests
{
    private readonly SalesRecordValidator _validator;

    public SalesRecordValidatorTests()
    {
        _validator = new SalesRecordValidator();
    }

    /// <summary>
    /// Tests that validation passes for a valid sales record.
    /// </summary>
    [Fact(DisplayName = "Valid sales record should pass all validation rules")]
    public void Given_ValidSalesRecord_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var salesRecord = SalesRecordTestData.GenerateValidSalesRecord();

        // Act
        var result = _validator.TestValidate(salesRecord);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for an invalid sale date.
    /// </summary>
    [Fact(DisplayName = "Invalid sale date should fail validation")]
    public void Given_InvalidSaleDate_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var salesRecord = SalesRecordTestData.GenerateValidSalesRecord();
        salesRecord.SaleDate = DateTime.MinValue; // Invalid date

        // Act
        var result = _validator.TestValidate(salesRecord);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleDate);
    }

    /// <summary>
    /// Tests that validation fails for an empty customer name.
    /// </summary>
    [Fact(DisplayName = "Empty customer name should fail validation")]
    public void Given_EmptyCustomerName_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var salesRecord = SalesRecordTestData.GenerateValidSalesRecord();
        salesRecord.CustomerName = ""; // Invalid: empty

        // Act
        var result = _validator.TestValidate(salesRecord);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    /// <summary>
    /// Tests that validation fails for a negative total sale amount.
    /// </summary>
    [Fact(DisplayName = "Negative total sale amount should fail validation")]
    public void Given_NegativeTotalSaleAmount_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var salesRecord = SalesRecordTestData.GenerateValidSalesRecord();
        salesRecord.TotalSaleAmount = -100.00m; // Invalid: negative value

        // Act
        var result = _validator.TestValidate(salesRecord);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TotalSaleAmount);
    }

    /// <summary>
    /// Tests that validation fails for an empty branch name.
    /// </summary>
    [Fact(DisplayName = "Empty branch name should fail validation")]
    public void Given_EmptyBranch_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var salesRecord = SalesRecordTestData.GenerateValidSalesRecord();
        salesRecord.Branch = ""; // Invalid: empty

        // Act
        var result = _validator.TestValidate(salesRecord);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Branch);
    }

    /// <summary>
    /// Tests that validation fails when there are no sale items.
    /// </summary>
    [Fact(DisplayName = "Empty sale items should fail validation")]
    public void Given_EmptySaleItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var salesRecord = SalesRecordTestData.GenerateValidSalesRecord();
        salesRecord.SaleItems.Clear(); // Invalid: no items

        // Act
        var result = _validator.TestValidate(salesRecord);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleItems);
    }
}