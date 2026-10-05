using MediatR;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    int Id,
    string Title
) : IRequest<BookDto>;