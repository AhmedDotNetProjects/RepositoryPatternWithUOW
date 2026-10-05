
namespace RepositoryPatternWithUOW.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IAuthorRepository Authors { get; }

    IBookRepository Books { get; }

    Task<int> Complete(
        CancellationToken cancellationToken = default);
}