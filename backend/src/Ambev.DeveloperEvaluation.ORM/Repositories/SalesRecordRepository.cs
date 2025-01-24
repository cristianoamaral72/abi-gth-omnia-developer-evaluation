using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SalesRecordRepository : ISalesRecordRepository
{
    private readonly DefaultContext _context;

    public SalesRecordRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SalesRecord> CreateAsync(SalesRecord salesRecord, CancellationToken cancellationToken = default)
    {
        await _context.SalesRecords.AddAsync(salesRecord, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return salesRecord;
    }

    public async Task<SalesRecord> UpdateAsync(SalesRecord salesRecord, CancellationToken cancellationToken = default)
    {
        if (salesRecord == null)
        {
            throw new ArgumentNullException(nameof(salesRecord), "The SalesRecord object cannot be null.");
        }

        var existingRecord = await _context.SalesRecords
            .Include(sr => sr.SaleItems) 
            .FirstOrDefaultAsync(sr => sr.Id == salesRecord.Id, cancellationToken);

        if (existingRecord == null)
        {
            throw new KeyNotFoundException($"SalesRecord with ID {salesRecord.Id} was not found.");
        }

        // Update scalar properties
        existingRecord.SaleDate = salesRecord.SaleDate;
        existingRecord.CustomerName = salesRecord.CustomerName;
        existingRecord.TotalSaleAmount = salesRecord.TotalSaleAmount;
        existingRecord.Branch = salesRecord.Branch;
        existingRecord.IsCancelled = salesRecord.IsCancelled;
        existingRecord.UpdatedAt = DateTime.UtcNow;

        // Update related SaleItems (if any)
        _context.SaleItems.RemoveRange(existingRecord.SaleItems); // Remove old items
        await _context.SaleItems.AddRangeAsync(salesRecord.SaleItems, cancellationToken); // Add new items

        await _context.SaveChangesAsync(cancellationToken); // Persist changes to the database

        return existingRecord;
    }


    public async Task<IEnumerable<SalesRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SalesRecords.ToListAsync(cancellationToken);
    }

    public async Task<SalesRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SalesRecords.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {

        var salesRecord = await GetByIdAsync(id, cancellationToken);
        if (salesRecord == null)
            return false;

        _context.SalesRecords.Remove(salesRecord);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}