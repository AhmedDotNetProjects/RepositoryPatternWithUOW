using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Common.Shared;
using RepositoryPatternWithUOW.Infranstructure.Data;

namespace RepositoryPatternWithUOW.Infranstructure.Repositories;

public class GenericRepository<T>(AppDbContext context)
    : IGenericRepository<T>
    where T : class
{
    private readonly AppDbContext _context = context;

    public async Task<T?> GetByIdAsync(
        int id,
        string[]? includes = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();

        ApplyIncludes(ref query, includes);

        return await query.SingleOrDefaultAsync(
            entity => EF.Property<int>(entity, "Id") == id,
            cancellationToken);
    }

    public async Task<T?> FindUsingExpressionAsync(
        Expression<Func<T, bool>> criteria,
        string[]? includes = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();

        ApplyIncludes(ref query, includes);

        return await query.SingleOrDefaultAsync(
            criteria,
            cancellationToken);
    }

    public async Task<ICollection<T>> GetAllAsync(
        string[]? includes = null,
        int? skip = null,
        int? take = null,
        Expression<Func<T, object>>? orderBy = null,
        string orderByDirection = "ASC",
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();

        ApplyIncludes(ref query, includes);

        if (orderBy is not null)
        {
            query = orderByDirection == OrderByDirection.ASC
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<T>()
            .AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<T>()
            .AddRangeAsync(entities, cancellationToken);

        return entities;
    }

    private static void ApplyIncludes(
        ref IQueryable<T> query,
        string[]? includes)
    {
        if (includes is null)
        {
            return;
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<int> Count(
        Expression<Func<T, bool>> criteria,
        CancellationToken cancellationToken)
    {
        return await _context.Set<T>().CountAsync(criteria, cancellationToken);
    }
}