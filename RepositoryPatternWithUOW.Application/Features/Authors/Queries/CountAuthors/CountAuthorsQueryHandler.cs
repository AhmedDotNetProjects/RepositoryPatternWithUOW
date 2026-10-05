using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Queries.CountAuthors;

public sealed class CountAuthorsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CountAuthorsQuery, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<int> Handle(
        CountAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.Authors.Count(
            _ => true,
            cancellationToken);
    }
}