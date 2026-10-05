using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Books.Commands.AddBooks;

public sealed class AddBooksCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddBooksCommand, IReadOnlyCollection<BookDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IReadOnlyCollection<BookDto>> Handle(
        AddBooksCommand command,
        CancellationToken cancellationToken)
    {
        var books = new List<Book>();

        foreach (var bookRequest in command.BookRequests)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(
                bookRequest.AuthorId,
                cancellationToken: cancellationToken);

            if (author is null)
            {
                throw new KeyNotFoundException(
                    $"Author with ID {bookRequest.AuthorId} was not found.");
            }

            books.Add(new Book
            {
                Title = bookRequest.Title,
                Author = author
            });
        }

        await _unitOfWork.Books.AddRangeAsync(
            books,
            cancellationToken);

        await _unitOfWork.Complete(cancellationToken);

        return books
            .Select(book => new BookDto(
                book.Id,
                book.Title,
                book.Author.Name))
            .ToList();
    }
}