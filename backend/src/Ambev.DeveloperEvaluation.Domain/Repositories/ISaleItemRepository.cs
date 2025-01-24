using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleItemRepository
{
    /// <summary>
    /// Creates a new SaleItem in the repository
    /// </summary>
    /// <param name="saleItem">The SaleItem to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created SaleItem</returns>
    Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing SaleItem in the repository
    /// </summary>
    /// <param name="saleItem"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new SaleItem in the repository
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<SaleItem>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a SaleItem by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the SaleItem</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The SaleItem if found, null otherwise</returns>
    Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a SaleItem from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the SaleItem to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the SaleItem was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}