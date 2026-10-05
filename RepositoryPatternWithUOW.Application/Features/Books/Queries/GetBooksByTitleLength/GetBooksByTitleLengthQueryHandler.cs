using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.GetBooksByTitleLength;

public sealed class GetBooksByTitleLengthQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<
        GetBooksByTitleLengthQuery,
        IReadOnlyCollection<BookDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyCollection<BookDto>> Handle(
        GetBooksByTitleLengthQuery query,
        CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books
            .GetBooksThatTitleLengthGraterThan(
                query.TitleLength,
                cancellationToken);

        return books
            .Select(book => new BookDto(
                book.Id,
                book.Title,
                book.Author.Name))
            .ToList();
    }
}