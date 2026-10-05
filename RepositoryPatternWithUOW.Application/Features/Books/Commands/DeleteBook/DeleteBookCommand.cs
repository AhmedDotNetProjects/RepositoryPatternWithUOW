using MediatR;

namespace RepositoryPatternWithUOW.Application.Features.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(
    int Id
) : IRequest;