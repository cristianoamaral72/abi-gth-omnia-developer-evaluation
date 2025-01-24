using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSalesHandlerTestData
{
    /// <summary>
    /// Generates a valid CreateSalesCommand with mocked SaleItems.
    /// </summary>
    public static CreateSalesCommand GenerateValidCommand()
    {
        return new CreateSalesCommand
        {
            SaleDate = DateTime.UtcNow,
            CustomerName = "John Doe",
            TotalSaleAmount = MockSaleItems().Sum(item => (item.UnitPrice * item.Quantity) - item.Discount),
            Branch = "New York",
            IsCancelled = false,
            SaleItems = MockSaleItems()
        };
    }

    /// <summary>
    /// Generates an invalid CreateSalesCommand with mocked SaleItems (e.g., quantity exceeds limit).
    /// </summary>
    public static CreateSalesCommand GenerateInvalidCommand()
    {
        return new CreateSalesCommand
        {
            SaleDate = DateTime.UtcNow,
            CustomerName = "John Doe",
            TotalSaleAmount = 200.00m,
            Branch = "New York",
            IsCancelled = false,
            SaleItems = MockInvalidSaleItems()
        };
    }

    /// <summary>
    /// Generates a valid list of mocked SaleItemCommand objects.
    /// </summary>
    public static ICollection<SaleItem> MockSaleItems()
    {
        return new List<SaleItem>
        {
            new SaleItem { ProductName = "Product A", Quantity = 5, UnitPrice = 10.00m, Discount = 5.00m },
            new SaleItem { ProductName = "Product B", Quantity = 10, UnitPrice = 7.00m, Discount = 7.00m }
        };
    }

    /// <summary>
    /// Generates an invalid list of mocked SaleItemCommand objects (e.g., invalid quantity).
    /// </summary>
    public static ICollection<SaleItem> MockInvalidSaleItems()
    {
        return new List<SaleItem>
        {
            new SaleItem { ProductName = "Product A", Quantity = 25, UnitPrice = 10.00m, Discount = 0.00m } // Invalid quantity
        };
    }
}

