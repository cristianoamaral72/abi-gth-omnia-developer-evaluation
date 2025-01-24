using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
    {
        await _context.SaleItems.AddAsync(saleItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return saleItem;
    }

    public async Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
    {
        if (saleItem == null)
        {
            throw new ArgumentNullException(nameof(saleItem), "The SaleItem object cannot be null.");
        }

        var existingItem = await _context.SaleItems
            .FirstOrDefaultAsync(item => item.Id == saleItem.Id, cancellationToken);

        if (existingItem == null)
        {
            throw new KeyNotFoundException($"SaleItem with ID {saleItem.Id} was not found.");
        }

        // Update scalar properties
        existingItem.ProductName = saleItem.ProductName;
        existingItem.Quantity = saleItem.Quantity;
        existingItem.UnitPrice = saleItem.UnitPrice;
        existingItem.Discount = saleItem.Discount;

        await _context.SaveChangesAsync(cancellationToken); // Persist changes to the database

        return existingItem; // Return the updated entity
    }


    public async Task<IEnumerable<SaleItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems.ToListAsync(cancellationToken);
    }

    public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var saleItem = await GetByIdAsync(id, cancellationToken);
        if (saleItem == null)
            return false;

        _context.SaleItems.Remove(saleItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}