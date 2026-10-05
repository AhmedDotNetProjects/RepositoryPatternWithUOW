using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.UpdateAuthor;

public sealed class UpdateAuthorCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthorDto> Handle(
        UpdateAuthorCommand command,
        CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(
            command.Id,
            ["Books"],
            cancellationToken);

        if (author is null)
        {
            throw new KeyNotFoundException(
                $"Author with id {command.Id} was not found.");
        }

        author.Name = command.Name;

        // author is tracked , so any change will be flaged that updated and when save changes will updated in database 
        // so, you can write or delete this :
        // _unitOfWork.Authors.Update(author);

        await _unitOfWork.Complete(cancellationToken);

        return new AuthorDto(
            author.Id,
            author.Name,
            author.Books.Select(b => b.Title));
    }
}