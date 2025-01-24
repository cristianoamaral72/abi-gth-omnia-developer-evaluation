namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetSalesResult
{
    public Guid Id { get; set; }

    public DateTime SaleDate { get; set; }

    public string CustomerName { get; set; }

    public decimal TotalSaleAmount { get; set; }

    public string Branch { get; set; }

    public bool IsCancelled { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
