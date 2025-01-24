using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

/// <summary>
/// Handler for processing DeleteSalesCommand requests
/// </summary>
public class DeleteSalesHandler : IRequestHandler<DeleteSalesCommand, DeleteSalesResponse>
{
    private readonly ISalesRecordRepository _salesRepository;

    /// <summary>
    /// Initializes a new instance of DeleteSalesHandler
    /// </summary>
    /// <param name="salesRepository">The Sales repository</param>
    /// <param name="validator">The validator for DeleteSalesCommand</param>
    public DeleteSalesHandler(
        ISalesRecordRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    /// <summary>
    /// Handles the DeleteSalesCommand request
    /// </summary>
    /// <param name="request">The DeleteSales command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteSalesResponse> Handle(DeleteSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSalesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _salesRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sales with ID {request.Id} not found");

        return new DeleteSalesResponse { Success = true };
    }
}
