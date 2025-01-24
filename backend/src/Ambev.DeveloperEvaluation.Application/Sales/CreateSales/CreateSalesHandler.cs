using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateSalesHandler : IRequestHandler<CreateSalesCommand, CreateSalesResult>
{
    private readonly ISalesRecordRepository _salesRecordRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateUserHandler
    /// </summary>
    /// <param name="salesRecordRepository"></param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateUserCommand</param>
    public CreateSalesHandler(ISalesRecordRepository salesRecordRepository, IMapper mapper)
    {
        _salesRecordRepository = salesRecordRepository;
        _mapper = mapper;
    }

    public async Task<CreateSalesResult> Handle(CreateSalesCommand command, CancellationToken cancellationToken)
    {
        // Validate the command
        var validator = new CreateSalesValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.ToString());

        // Process SaleItems and apply business rules
        foreach (var item in command.SaleItems)
        {
            if (item.Quantity < 4)
            {
                item.Discount = 0; // No discount for less than 4 items
            }
            else if (item.Quantity >= 4 && item.Quantity < 10)
            {
                item.Discount = item.UnitPrice * item.Quantity * 0.10m; // 10% discount for 4-9 items
            }
            else if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                item.Discount = item.UnitPrice * item.Quantity * 0.20m; // 20% discount for 10-20 items
            }
            else if (item.Quantity > 20)
            {
                throw new InvalidOperationException($"Cannot sell more than 20 items of {item.ProductName}.");
            }

            // Ensure the total price for the item is calculated correctly
            item.ItemTotal = (item.UnitPrice * item.Quantity) - item.Discount;
        }

        // Create a new SalesRecord entity
        var salesRecord = new SalesRecord
        {
            SaleDate = command.SaleDate,
            CustomerName = command.CustomerName,
            TotalSaleAmount = command.SaleItems.Sum(i => i.ItemTotal),
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
            SaleItems = command.SaleItems.Select(item => new SaleItem
            {
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                ItemTotal = item.ItemTotal
            }).ToList()
        };

        // Save the sales record in the repository
        var createdSalesRecord = await _salesRecordRepository.CreateAsync(salesRecord, cancellationToken);

        // Map the created sales record to the result DTO
        var result = _mapper.Map<CreateSalesResult>(createdSalesRecord);
        return result;
    }

}
