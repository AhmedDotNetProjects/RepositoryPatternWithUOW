using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.GetBookById;

public sealed class GetByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetBookByIdQuery, BookDto?>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BookDto?> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(query.Id, ["Author"], cancellationToken);

        if (book is null)
            return null;

        return new BookDto(book.Id, book.Title, book.Author.Name);
    }
}