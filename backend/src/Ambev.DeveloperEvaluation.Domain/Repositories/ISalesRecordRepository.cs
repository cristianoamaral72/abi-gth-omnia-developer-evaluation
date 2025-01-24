using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISalesRecordRepository
{
    /// <summary>
    /// Creates a new SalesRecord in the repository
    /// </summary>
    /// <param name="salesRecord">The SalesRecord to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created SalesRecord</returns>
    Task<SalesRecord> CreateAsync(SalesRecord salesRecord, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing SalesRecord in the repository
    /// </summary>
    /// <param name="salesRecord"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SalesRecord> UpdateAsync(SalesRecord salesRecord, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new SalesRecord in the repository
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<SalesRecord>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a SalesRecord by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the SalesRecord</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The SalesRecord if found, null otherwise</returns>
    Task<SalesRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a SalesRecord from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the SalesRecord to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the SalesRecord was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}