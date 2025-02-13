using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSalesHandler"/> class.
/// </summary>
public class CreateSalesHandlerTests
{
    private readonly ISalesRecordRepository _salesRecordRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateSalesCommand> _validator;
    private readonly CreateSalesHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSalesHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSalesHandlerTests()
    {
        _salesRecordRepository = Substitute.For<ISalesRecordRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<IValidator<CreateSalesCommand>>();
        _handler = new CreateSalesHandler(_salesRecordRepository, _mapper, _validator);
    }

    [Fact(DisplayName = "Given valid sales data When creating sales Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSalesHandlerTestData.GenerateValidCommand();
        var salesRecord = new SalesRecord
        {
            Id = Guid.NewGuid(),
            SaleDate = command.SaleDate,
            CustomerName = command.CustomerName,
            TotalSaleAmount = command.TotalSaleAmount,
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
            SaleItems = command.SaleItems.Select(si => new SaleItem
            {
                ProductName = si.ProductName,
                Quantity = si.Quantity,
                UnitPrice = si.UnitPrice,
                Discount = si.Discount
            }).ToList()
        };

        var result = new CreateSalesResult { Id = salesRecord.Id };

        _validator.ValidateAsync(command, CancellationToken.None).Returns(new ValidationResult());
        _mapper.Map<SalesRecord>(command).Returns(salesRecord);
        _mapper.Map<CreateSalesResult>(salesRecord).Returns(result);
        _salesRecordRepository.CreateAsync(Arg.Any<SalesRecord>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(salesRecord));

        // When
        var createSalesResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSalesResult.Should().NotBeNull();
        createSalesResult.Id.Should().Be(salesRecord.Id);
    }


    [Fact(DisplayName = "Given invalid sales data When creating sales Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSalesCommand(); // Empty command will fail validation
        _validator.ValidateAsync(command, CancellationToken.None).Returns(new ValidationResult(new[] { new ValidationFailure("CustomerName", "Customer name is required.") }));

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given invalid command with more than 20 items When handling Then throws InvalidOperationException")]
    public async Task Handle_ValidRequest_MapsCommandToSalesRecord()
    {
        // Given
        var command = CreateSalesHandlerTestData.GenerateInvalidCommand(); // Gera comando inválido
        _validator.ValidateAsync(command, CancellationToken.None).Returns(new ValidationResult());

        // When
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        var response = await act.Should().ThrowAsync<InvalidOperationException>();
        response.And.Message.Contains("Cannot sell more than 20 items of Product A.");
    }


    [Fact(DisplayName = "Given invalid sale item data When creating sales Then throws exception")]
    public async Task Handle_InvalidSaleItemData_ThrowsException()
    {
        // Given
        var command = CreateSalesHandlerTestData.GenerateInvalidCommand(); // Gera comando inválido
        _validator.ValidateAsync(command, CancellationToken.None).Returns(new ValidationResult());

        // When
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        var response = await act.Should().ThrowAsync<InvalidOperationException>();
        response.And.Message.Contains("Cannot sell more than 20 items of");
    }
}
