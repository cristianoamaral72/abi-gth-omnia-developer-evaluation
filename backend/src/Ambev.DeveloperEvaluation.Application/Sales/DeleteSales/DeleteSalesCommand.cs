using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

/// <summary>
/// Command for deleting a sales
/// </summary>
public record DeleteSalesCommand : IRequest<DeleteSalesResponse>
{
    /// <summary>
    /// The unique identifier of the sales to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteSalesCommand
    /// </summary>
    /// <param name="id">The ID of the sales to delete</param>
    public DeleteSalesCommand(Guid id)
    {
        Id = id;
    }
}
