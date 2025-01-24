using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CreateSalesRequest
{
    public Guid Id { get; set; }

    public DateTime SaleDate { get; set; }

    public string CustomerName { get; set; }

    public decimal TotalSaleAmount { get; set; }

    public string Branch { get; set; }

    public bool IsCancelled { get; set; } = false;
}