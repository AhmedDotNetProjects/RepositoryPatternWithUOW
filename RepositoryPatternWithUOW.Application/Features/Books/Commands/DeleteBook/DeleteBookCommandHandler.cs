using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;

namespace RepositoryPatternWithUOW.Application.Features.Books.Commands.DeleteBook;

public sealed class DeleteBookCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteBookCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(
        DeleteBookCommand command,
        CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(
            command.Id,
            cancellationToken: cancellationToken);

        if (book is null)
        {
            throw new KeyNotFoundException(
                $"Book with id {command.Id} was not found.");
        }

        _unitOfWork.Books.Delete(book);

        await _unitOfWork.Complete(cancellationToken);
    }
}