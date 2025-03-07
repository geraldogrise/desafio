using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Carrefour.Desafio.ORM.Repositories;

/// <summary>
/// Implementation of IConsolidadoRepository using Entity Framework Core
/// </summary>
public class ConsolidadoRepository : IConsolidadoRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ConsolidadoRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ConsolidadoRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new consolidado in the database
    /// </summary>
    /// <param name="consolidado">The consolidado to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created consolidado</returns>
    public async Task<Consolidado> CreateAsync(Consolidado consolidado, CancellationToken cancellationToken = default)
    {
        await _context.Consolidados.AddAsync(consolidado, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return consolidado;
    }

    /// <summary>
    /// Updates a specific consolidado in the database
    /// </summary>
    /// <param name="id">The unique identifier of the consolidado</param>
    /// <param name="consolidado">The consolidado data to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated consolidado</returns>
    public async Task<Consolidado?> UpdateAsync(Guid id, Consolidado consolidado, CancellationToken cancellationToken = default)
    {
        var existingConsolidado = await _context.Consolidados.FindAsync(new object[] { id }, cancellationToken);
        if (existingConsolidado == null)
            return null;

        _context.Entry(existingConsolidado).CurrentValues.SetValues(consolidado);
        await _context.SaveChangesAsync(cancellationToken);

        return existingConsolidado;
    }


    /// <summary>
    /// Retrieves a consolidado by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the consolidado</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The consolidado if found, null otherwise</returns>
    public async Task<Consolidado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Consolidados.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }



    /// <summary>
    /// Retrieves a consolidado by their unique date
    /// </summary>
    /// <param name="dataConsolidado">The data de consolidação of the consolidado</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The consolidado if found, null otherwise</returns>
    public async Task<Consolidado?> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.Consolidados.FirstOrDefaultAsync(o => o.DataConsolidado.Date == date.Date, cancellationToken);
    }


    /// <summary>
    /// Deletes a consolidado from the database
    /// </summary>
    /// <param name="id">The unique identifier of the consolidado to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the consolidado was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var consolidado = await GetByIdAsync(id, cancellationToken);
        if (consolidado == null)
            return false;

        _context.Consolidados.Remove(consolidado);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a list of consolidados with pagination and ordering
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of items per page</param>
    /// <param name="order">Ordering of the results</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of consolidados with pagination info</returns>
    public async Task<Carrefour.Desafio.Common.Result.PagedResult<Consolidado>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default)
    {
        var query = _context.Consolidados.AsQueryable();

        // Ordenação dinâmica usando System.Linq.Dynamic.Core
        if (!string.IsNullOrWhiteSpace(order))
        {
            query = query.OrderBy(order);
        }

        var totalItems = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);

        return new Carrefour.Desafio.Common.Result.PagedResult<Consolidado>
        {
            Data = items,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)size)
        };
    }
}
