using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.DeleteAuthor;

public sealed class DeleteAuthorCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(
        DeleteAuthorCommand command,
        CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(
            command.Id,
            cancellationToken: cancellationToken);

        if (author is null)
        {
            throw new KeyNotFoundException(
                $"Author with id {command.Id} was not found.");
        }

        _unitOfWork.Authors.Delete(author);

        await _unitOfWork.Complete(cancellationToken);
    }
}