using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.UpdateBooksByAuthor;

public sealed class UpdateBooksByAuthorCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<
        UpdateBooksByAuthorCommand,
        IReadOnlyCollection<BookDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyCollection<BookDto>> Handle(
        UpdateBooksByAuthorCommand command,
        CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(
            command.AuthorId,
            ["Books"],
            cancellationToken);

        if (author is null)
        {
            throw new KeyNotFoundException(
                $"Author with id {command.AuthorId} was not found.");
        }

        foreach (var requestBook in command.Books)
        {
            var book = author.Books
                .FirstOrDefault(b => b.Id == requestBook.Id);

            if (book is null)
            {
                continue;
            }

            book.Title = requestBook.Title;
        }

        await _unitOfWork.Complete(cancellationToken);

        return author.Books
            .Select(book => new BookDto(
                book.Id,
                book.Title,
                author.Name))
            .ToList();
    }
}