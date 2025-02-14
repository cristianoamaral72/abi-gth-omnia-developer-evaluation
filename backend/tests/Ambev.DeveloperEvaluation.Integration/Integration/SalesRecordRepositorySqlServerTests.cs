using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Integration;

public class SalesRecordRepositorySqlServerTests : IClassFixture<SqlServerTestFixture>
{
    private readonly DefaultContext _dbContext;

    public SalesRecordRepositorySqlServerTests(SqlServerTestFixture fixture)
    {
        _dbContext = fixture.DbContext; // Usa o contexto de banco da fixture
    }

    [Fact(DisplayName = "Given a valid sales record When saving to SQL Server Then it should persist correctly")]
    public async Task Given_ValidSalesRecord_When_Saving_Then_ShouldPersist()
    {
        // Given
        var salesRecord = new SalesRecord
        {
            Id = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            CustomerName = "John Doe",
            TotalSaleAmount = 500m,
            Branch = "Main Store",
            IsCancelled = false
        };

        // When
        _dbContext.SalesRecords.Add(salesRecord);
        await _dbContext.SaveChangesAsync();

        var retrievedRecord = await _dbContext.SalesRecords.FindAsync(salesRecord.Id);

        // Then
        retrievedRecord.Should().NotBeNull();
        retrievedRecord.CustomerName.Should().Be("John Doe");
        retrievedRecord.TotalSaleAmount.Should().Be(500m);
        retrievedRecord.Branch.Should().Be("Main Store");
    }
}