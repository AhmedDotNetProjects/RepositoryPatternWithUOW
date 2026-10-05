using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Infranstructure.Data;

namespace RepositoryPatternWithUOW.Infranstructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IAuthorRepository Authors { get; }

    public IBookRepository Books { get; }

    public UnitOfWork(AppDbContext context, IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        _context = context;
        Authors = authorRepository;
        Books = bookRepository;
    }

    public Task<int> Complete(
        CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}