using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Common.Shared;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.GetAllBooks;

public sealed class GetAllBooksQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>?>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<BookDto>?> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var bookList = await _unitOfWork.Books.GetAllAsync(["Author"], null, null, b => b.Id, OrderByDirection.ASC, cancellationToken);

        if (bookList is null)
            return null;

        return bookList.Select(x => new BookDto(x.Id, x.Title, x.Author.Name));
    }
}