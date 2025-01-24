namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SalesRecord
{
    public Guid Id { get; set; } 

    public DateTime SaleDate { get; set; } 

    public string CustomerName { get; set; }

    public decimal TotalSaleAmount { get; set; }

    public string Branch { get; set; }

    public bool IsCancelled { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

    public DateTime UpdatedAt { get; set; }

    // Navigation property for related items
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}