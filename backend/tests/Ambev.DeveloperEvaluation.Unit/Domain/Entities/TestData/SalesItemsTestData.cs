using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class SalesItemsTestData
{
    public static SaleItem GenerateValidSaleItem()
    {
        return new SaleItem
        {
            ProductName = "Product A",
            Quantity = 5,
            UnitPrice = 10.00m,
            Discount = 5.00m
        };
    }

    public static SaleItem GenerateInvalidSaleItem()
    {
        return new SaleItem
        {
            ProductName = "",
            Quantity = 25,
            UnitPrice = 0.00m,
            Discount = -5.00m
        };
    }
}