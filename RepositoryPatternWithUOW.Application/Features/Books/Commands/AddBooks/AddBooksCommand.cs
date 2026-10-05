using MediatR;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Books.Commands.AddBooks;

public sealed record AddBooksCommand(IReadOnlyCollection<BookRequest> BookRequests) : IRequest<IReadOnlyCollection<BookDto>>;