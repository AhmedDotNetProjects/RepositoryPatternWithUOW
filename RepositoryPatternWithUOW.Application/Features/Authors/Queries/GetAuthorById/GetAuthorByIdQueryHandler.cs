using MediatR;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Authors.Queries.GetAuthorById;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

public sealed class GetByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAuthorByIdQuery, AuthorDto?>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthorDto?> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
    {

        var author = await _unitOfWork.Authors.GetByIdAsync(query.Id, ["Books"], cancellationToken);

        if (author is null)
            return null;

        return new AuthorDto(author.Id, author.Name, author.Books.Select(x => x.Title));
    }
}