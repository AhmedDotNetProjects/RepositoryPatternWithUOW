using System.Linq.Expressions;

namespace RepositoryPatternWithUOW.Application.Common.Interfaces;

public interface IGenericRepository<T>
    where T : class
{
    Task<T?> GetByIdAsync(
        int id,
        string[]? includes = null,
        CancellationToken cancellationToken = default);

    Task<ICollection<T>> GetAllAsync(
        string[]? includes = null,
        int? skip = null,
        int? take = null,
        Expression<Func<T, object>>? orderBy = null,
        string orderByDirection = "ASC",
        CancellationToken cancellationToken = default);

    Task<T?> FindUsingExpressionAsync(
        Expression<Func<T, bool>> criteria,
        string[]? includes = null,
        CancellationToken cancellationToken = default);

    Task<T> AddAsync(
        T entity,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> AddRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default);

    void Update(T entity);

    void Delete(T entity);

    Task<int> Count(
        Expression<Func<T, bool>> criteria,
        CancellationToken cancellationToken);
}