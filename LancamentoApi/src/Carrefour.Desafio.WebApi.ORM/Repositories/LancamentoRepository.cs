using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Carrefour.Desafio.ORM.Repositories;

/// <summary>
/// Implementation of ILancamentoRepository using Entity Framework Core
/// </summary>
public class LancamentoRepository : ILancamentoRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of LancamentoRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public LancamentoRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<Lancamento> CreateAsync(Lancamento user, CancellationToken cancellationToken = default)
    {
        await _context.Lancamentos.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Updates a specific user in the database
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="user">The user data to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user</returns>
    public async Task<Lancamento?> UpdateAsync(Guid id, Lancamento user, CancellationToken cancellationToken = default)
    {
        var existingLancamento = await _context.Lancamentos.FindAsync(new object[] { id }, cancellationToken);
        if (existingLancamento == null)
            return null;

        _context.Entry(existingLancamento).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync(cancellationToken);

        return existingLancamento;
    }


    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<Lancamento?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Lancamentos.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Lancamentos.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a list of users with pagination and ordering
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of items per page</param>
    /// <param name="order">Ordering of the results</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of users with pagination info</returns>
    public async Task<Carrefour.Desafio.Common.Result.PagedResult<Lancamento>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default)
    {
        var query = _context.Lancamentos.AsQueryable();

        // Ordenação dinâmica usando System.Linq.Dynamic.Core
        if (!string.IsNullOrWhiteSpace(order))
        {
            query = query.OrderBy(order);
        }

        var totalItems = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);

        return new Carrefour.Desafio.Common.Result.PagedResult<Lancamento>
        {
            Data = items,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)size)
        };
    }
}
