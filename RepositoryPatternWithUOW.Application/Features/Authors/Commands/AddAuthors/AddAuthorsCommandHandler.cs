using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Authors.Commands.AddAuthor;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.AddAuthors;

public sealed class AddAuthorsCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddAuthorsCommand, IReadOnlyCollection<AuthorDto>>
{
    public async Task<IReadOnlyCollection<AuthorDto>> Handle(AddAuthorsCommand command, CancellationToken cancellationToken)
    {
        var authors = command.Authors.Select(authorRequest => new Author
        {
            Name = authorRequest.Name,
            Books = authorRequest.BookTitles
                .Select(title => new Book
                {
                    Title = title
                }).ToList()
        }).ToList();

        await unitOfWork.Authors.AddRangeAsync(authors, cancellationToken);
        await unitOfWork.Complete(cancellationToken);

        return authors.Select(author => new AuthorDto(
            author.Id,
            author.Name,
            author.Books.Select(book => book.Title)))
            .ToList();
    }
}