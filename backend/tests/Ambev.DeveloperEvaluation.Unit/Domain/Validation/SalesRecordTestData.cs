namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;

public static class SalesRecordTestData
{
    public static SalesRecord GenerateValidSalesRecord()
    {
        return new SalesRecord
        {
            Id = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            CustomerName = "John Doe",
            TotalSaleAmount = 100.00m,
            Branch = "New York",
            IsCancelled = false,
            SaleItems = new List<SaleItem>
            {
                new SaleItem { ProductName = "Product A", Quantity = 2, UnitPrice = 50.00m, Discount = 0.00m }
            }
        };
    }
}
