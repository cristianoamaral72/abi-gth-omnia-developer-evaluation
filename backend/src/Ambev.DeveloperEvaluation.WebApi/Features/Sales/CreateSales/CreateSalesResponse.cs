using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class CreateSalesResponse
{
    public Guid Id { get; set; }

    public DateTime SaleDate { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public decimal TotalSaleAmount { get; set; }

    public string Branch { get; set; } = string.Empty;

    public bool IsCancelled { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }

    // Navigation property for related items
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}
