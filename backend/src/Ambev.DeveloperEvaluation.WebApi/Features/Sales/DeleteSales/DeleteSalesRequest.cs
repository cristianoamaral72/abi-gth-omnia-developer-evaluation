namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSales;

/// <summary>
/// Request model for deleting a Sales
/// </summary>
public class DeleteSalesRequest
{
    /// <summary>
    /// The unique identifier of the Sales to delete
    /// </summary>
    public Guid Id { get; set; }
}
