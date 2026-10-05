using MediatR;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Books.Commands.AddBook;

public sealed record AddBookCommand(
    BookRequest Book
) : IRequest<BookDto>;