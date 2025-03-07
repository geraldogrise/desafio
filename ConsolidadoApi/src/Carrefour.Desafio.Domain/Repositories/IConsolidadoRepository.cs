using Carrefour.Desafio.Domain.Entities;
using System.Linq.Dynamic.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Carrefour.Desafio.Domain.Repositories;

/// <summary>
/// Repository interface for Consolidado entity operations
/// </summary>
public interface IConsolidadoRepository
{
    /// <summary>
    /// Creates a new consolidado in the repository
    /// </summary>
    /// <param name="consolidado">The consolidado to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created consolidado</returns>
    Task<Consolidado> CreateAsync(Consolidado consolidado, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a specific consolidado in the repository
    /// </summary>
    /// <param name="id">The unique identifier of the consolidado</param>
    /// <param name="consolidado">The consolidado data to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated consolidado</returns>
    Task<Consolidado?> UpdateAsync(Guid id, Consolidado consolidado, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a consolidado by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the consolidado</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The consolidado if found, null otherwise</returns>
    Task<Consolidado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a consolidado by their unique identifier
    /// </summary>
    /// <param name="dataConsolidada">The date of the consolidado</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The consolidado if found, null otherwise</returns>
    Task<Consolidado?> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a consolidado from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the consolidado to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the consolidado was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of consolidados with pagination and ordering
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of items per page</param>
    /// <param name="order">Ordering of the results</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of consolidados with pagination info</returns>
    public Task<Carrefour.Desafio.Common.Result.PagedResult<Consolidado>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default);
}
