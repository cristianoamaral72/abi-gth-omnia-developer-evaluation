using Ambev.DeveloperEvaluation.Unit.Application.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application;

using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


/// <summary>
/// Contains unit tests for the <see cref="CreateSalesHandler"/> class.
/// </summary>
public class CreateSalesHandlerTests
{
    private readonly ISalesRecordRepository _salesRecordRepository;
    private readonly IMapper _mapper;
    private readonly CreateSalesHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSalesHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSalesHandlerTests()
    {
        _salesRecordRepository = Substitute.For<ISalesRecordRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSalesHandler(_salesRecordRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid sales creation request is handled successfully.
    /// </summary>
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

        var result = new CreateSalesResult
        {
            Id = salesRecord.Id
        };

        _mapper.Map<SalesRecord>(command).Returns(salesRecord);
        _mapper.Map<CreateSalesResult>(salesRecord).Returns(result);
        _salesRecordRepository.CreateAsync(Arg.Any<SalesRecord>(), Arg.Any<CancellationToken>())
            .Returns(salesRecord);

        // When
        var createSalesResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSalesResult.Should().NotBeNull();
        createSalesResult.Id.Should().Be(salesRecord.Id);
        await _salesRecordRepository.Received(1).CreateAsync(Arg.Any<SalesRecord>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sales creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sales data When creating sales Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSalesCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to sales record entity")]
    public async Task Handle_ValidRequest_MapsCommandToSalesRecord()
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

        _mapper.Map<SalesRecord>(command).Returns(salesRecord);
        _salesRecordRepository.CreateAsync(Arg.Any<SalesRecord>(), Arg.Any<CancellationToken>())
            .Returns(salesRecord);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<SalesRecord>(Arg.Is<CreateSalesCommand>(c =>
            c.SaleDate == command.SaleDate &&
            c.CustomerName == command.CustomerName &&
            c.TotalSaleAmount == command.TotalSaleAmount &&
            c.Branch == command.Branch));
    }

    /// <summary>
    /// Tests that a sale with an invalid number of items throws an exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale item data When creating sales Then throws exception")]
    public async Task Handle_InvalidSaleItemData_ThrowsException()
    {
        // Given
        var command = CreateSalesHandlerTestData.GenerateInvalidCommand(); // Invalid item quantity > 20

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Cannot sell more than 20 items of a single product.");
    }
}
