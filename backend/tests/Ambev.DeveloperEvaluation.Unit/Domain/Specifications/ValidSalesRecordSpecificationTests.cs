using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;


namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class ValidSalesRecordSpecificationTests
{
    [Theory]
    [InlineData(false, 100.00, 1, true)]  // Valid sales record
    [InlineData(true, 100.00, 1, false)]  // Invalid: Sale is canceled
    [InlineData(false, 0.00, 1, false)]   // Invalid: TotalSaleValue is zero
    [InlineData(false, 100.00, 0, false)] // Invalid: No sale items
    public void IsSatisfiedBy_ShouldValidateSalesRecord(
        bool isCancelled,
        decimal totalSaleValue,
        int numberOfItems,
        bool expectedResult)
    {
        // Arrange
        var salesRecord = ValidSalesRecordSpecificationTestData.GenerateSalesRecord(
            isCancelled,
            totalSaleValue,
            numberOfItems);
        var specification = new ValidSalesRecordSpecification();

        // Act
        var result = specification.IsSatisfiedBy(salesRecord);

        // Assert
        result.Should().Be(expectedResult);
    }
}

public class ValidSalesRecordSpecification
{
    public bool IsSatisfiedBy(SalesRecord salesRecord)
    {
        if (salesRecord == null) return false;

        return !salesRecord.IsCancelled &&
               salesRecord.TotalSaleAmount > 0 &&
               salesRecord.SaleItems != null &&
               salesRecord.SaleItems.Any();
    }
}
