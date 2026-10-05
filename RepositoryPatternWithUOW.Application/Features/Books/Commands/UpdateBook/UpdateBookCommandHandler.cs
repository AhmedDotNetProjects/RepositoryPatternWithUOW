using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateBookCommand, BookDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BookDto> Handle(
        UpdateBookCommand command,
        CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(
            command.Id,
            ["Author"],
            cancellationToken);

        if (book is null)
        {
            throw new KeyNotFoundException(
                $"Book with id {command.Id} was not found.");
        }

        book.Title = command.Title;
        // _unitOfWork.Update(book);

        await _unitOfWork.Complete(cancellationToken);

        return new BookDto(
            book.Id,
            book.Title,
            book.Author.Name);
    }
}