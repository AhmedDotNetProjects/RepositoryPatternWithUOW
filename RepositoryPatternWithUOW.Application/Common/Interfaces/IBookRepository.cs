using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Common.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<IEnumerable<Book>> GetBooksThatTitleLengthGraterThan(int titleLength, CancellationToken cancellationToken);
}