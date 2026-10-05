using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Application.Common.Shared;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Queries.GetAllAuthors;

public sealed class GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto?>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<AuthorDto?>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authorList = await _unitOfWork.Authors.GetAllAsync(["Books"], null, null, b => b.Id, OrderByDirection.ASC, cancellationToken);

        return authorList.Select(x => new AuthorDto(x.Id, x.Name, x.Books.Select(x => x.Title)));
    }
}