using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

/// <summary>
/// Handler for processing CreateSalesCommand requests.
/// </summary>
public class CreateSalesHandler : IRequestHandler<CreateSalesCommand, CreateSalesResult>
{
    private readonly ISalesRecordRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateSalesCommand> _validator;

    /// <summary>
    /// Initializes a new instance of CreateSalesHandler.
    /// </summary>
    public CreateSalesHandler(ISalesRecordRepository repository, IMapper mapper, IValidator<CreateSalesCommand> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CreateSalesResult> Handle(CreateSalesCommand command, CancellationToken cancellationToken)
    {
        // Validate the command using FluentValidation
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        // Apply business rules to SaleItems
        foreach (var item in command.SaleItems)
        {
            item.Discount = item.Quantity switch
            {
                < 4 => 0,
                >= 4 and < 10 => item.UnitPrice * item.Quantity * 0.10m,
                >= 10 and <= 20 => item.UnitPrice * item.Quantity * 0.20m,
                > 20 => throw new InvalidOperationException($"Cannot sell more than 20 items of {item.ProductName}."),
            };
        }

        // Ensure mapping configuration is working correctly
        var salesRecord = _mapper.Map<SalesRecord>(command);
        if (salesRecord == null)
        {
            throw new NullReferenceException("Failed to map CreateSalesCommand to SalesRecord. Ensure AutoMapper is configured correctly.");
        }

        // Manually ensure SaleItems are mapped properly
        salesRecord.SaleItems = command.SaleItems.Select(si => new SaleItem
        {
            ProductName = si.ProductName,
            Quantity = si.Quantity,
            UnitPrice = si.UnitPrice,
            Discount = si.Discount
        }).ToList();

        // Update total sale amount
        salesRecord.TotalSaleAmount = salesRecord.SaleItems.Sum(i => (i.UnitPrice * i.Quantity) - i.Discount);

        // Save to repository
        var createdRecord = await _repository.CreateAsync(salesRecord, cancellationToken);
        if (createdRecord == null)
        {
            throw new NullReferenceException("Repository returned null when creating SalesRecord.");
        }

        // Map the created record to result DTO
        var result = _mapper.Map<CreateSalesResult>(createdRecord);
        if (result == null)
        {
            throw new NullReferenceException("Failed to map SalesRecord to CreateSalesResult.");
        }

        return result;
    }

}
