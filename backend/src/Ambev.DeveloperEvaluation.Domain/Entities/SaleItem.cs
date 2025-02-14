namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public Guid Id { get; set; } 

    public int SaleID { get; set; } 

    public string ProductName { get; set; } 

    public int Quantity { get; set; } 

    public decimal UnitPrice { get; set; } 

    public decimal Discount { get; set; } = 0.00m;

    // Propriedade calculada ao invés de ser definida diretamente
    public decimal ItemTotal => (UnitPrice * Quantity) - Discount;

    public SalesRecord SalesRecord { get; set; }

    public bool IsValid()
    {
        // Validate each property of the SaleItemCommand
        if (string.IsNullOrWhiteSpace(ProductName))
            return false; // ProductName must not be null, empty, or whitespace

        if (Quantity <= 0 || Quantity > 20)
            return false; // Quantity must be greater than 0 and less than or equal to 20

        if (UnitPrice <= 0)
            return false; // UnitPrice must be greater than 0

        if (Discount < 0)
            return false; // Discount must not be negative

        // All validations passed
        return true;
    }

}