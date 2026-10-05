using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.FindBookUsingExpression;

public sealed class FindBookUsingExpressionQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<FindBookUsingExpressionQuery, BookDto?>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BookDto?> Handle(FindBookUsingExpressionQuery query, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.FindUsingExpressionAsync(query.Match);
        if (book is null)
            return null;
        return new BookDto(book.Id, book.Title, book.Author.Name);
    }
}