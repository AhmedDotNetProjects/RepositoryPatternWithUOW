using MediatR;

using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Books.Commands.AddBook;

public sealed class AddBookCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddBookCommand, BookDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BookDto> Handle(
        AddBookCommand command,
        CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(
            command.Book.AuthorId,
            cancellationToken: cancellationToken);

        if (author is null)
        {
            throw new KeyNotFoundException(
                $"Author with ID {command.Book.AuthorId} was not found.");
        }

        var book = new Book
        {
            Title = command.Book.Title,
            Author = author
        };

        await _unitOfWork.Books.AddAsync(
            book,
            cancellationToken);

        await _unitOfWork.Complete(cancellationToken);

        return new BookDto(
            book.Id,
            book.Title,
            book.Author.Name);
    }
}