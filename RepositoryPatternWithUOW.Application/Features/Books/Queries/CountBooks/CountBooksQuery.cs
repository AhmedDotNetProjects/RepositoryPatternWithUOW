using MediatR;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.CountBooks;

public sealed record CountBooksQuery : IRequest<int>;