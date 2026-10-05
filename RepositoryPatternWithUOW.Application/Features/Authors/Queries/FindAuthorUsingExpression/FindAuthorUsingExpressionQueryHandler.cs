using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Queries.FindAuthorUsingExpression;

public sealed class FindAuthorUsingExpressionQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<FindAuthorUsingExpressionQuery, AuthorDto?>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthorDto?> Handle(FindAuthorUsingExpressionQuery query, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.FindUsingExpressionAsync(query.Match);

        if (author is null)
            return null;

        return new AuthorDto(author.Id, author.Name, author.Books.Select(x => x.Title));
    }
}