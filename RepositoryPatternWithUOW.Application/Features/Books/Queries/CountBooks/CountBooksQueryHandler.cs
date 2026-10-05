using MediatR;
using RepositoryPatternWithUOW.Application.Common.Interfaces;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.CountBooks;

public sealed class CountBooksQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CountBooksQuery, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<int> Handle(
        CountBooksQuery request,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.Books.Count(
            _ => true,
            cancellationToken);
    }
}