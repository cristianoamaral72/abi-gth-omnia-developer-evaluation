using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Command for retrieving a sales by their ID
/// </summary>
public record GetSalesCommand : IRequest<GetSalesResult>
{
    /// <summary>
    /// The unique identifier of the sales to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetSalesCommand
    /// </summary>
    /// <param name="id">The ID of the sales to retrieve</param>
    public GetSalesCommand(Guid id)
    {
        Id = id;
    }
}
