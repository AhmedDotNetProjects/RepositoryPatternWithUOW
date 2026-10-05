using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Infranstructure.Data;

namespace RepositoryPatternWithUOW.Infranstructure.Repositories;

public class BookRepository(AppDbContext context) : GenericRepository<Book>(context), IBookRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Book>> GetBooksThatTitleLengthGraterThan(
        int titleLength,
        CancellationToken cancellationToken)
    {
        return await _context.Books.Where(b => b.Title.Length > titleLength).ToListAsync(cancellationToken);
    }
}