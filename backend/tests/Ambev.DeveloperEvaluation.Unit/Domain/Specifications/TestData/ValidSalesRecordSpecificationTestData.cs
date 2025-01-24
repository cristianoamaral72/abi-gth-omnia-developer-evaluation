
using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

public static class ValidSalesRecordSpecificationTestData
{
    public static SalesRecord GenerateSalesRecord(bool isCancelled, decimal totalSaleValue, int numberOfItems)
    {
        return new SalesRecord
        {
            Id = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            CustomerName = "Test Customer",
            TotalSaleAmount = totalSaleValue,
            Branch = "Test Branch",
            IsCancelled = isCancelled,
            SaleItems = GenerateSaleItems(numberOfItems)
        };
    }

    private static List<SaleItem> GenerateSaleItems(int numberOfItems)
    {
        var saleItems = new List<SaleItem>();

        for (int i = 0; i < numberOfItems; i++)
        {
            saleItems.Add(new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductName = $"Product {i + 1}",
                Quantity = 1,
                UnitPrice = 10.00m,
                Discount = 0.00m
            });
        }

        return saleItems;
    }
}
