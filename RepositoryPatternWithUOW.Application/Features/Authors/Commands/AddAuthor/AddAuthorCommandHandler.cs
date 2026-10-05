using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.AddAuthor;

public sealed class AddAuthorCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthorDto> Handle(AddAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Name = command.Author.Name,
            Books = command.Author.BookTitles
                        .Select(bookTitle
                            => new Book
                            {
                                Title = bookTitle
                            }).ToList()
        };

        await _unitOfWork.Authors.AddAsync(author, cancellationToken);
        await _unitOfWork.Complete(cancellationToken);

        return new AuthorDto
        (
            author.Id,
            author.Name,
            author.Books.Select(b => b.Title).ToList()
        );
    }
}