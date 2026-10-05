using MediatR;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.UpdateBooksByAuthor;

public sealed record UpdateBooksByAuthorCommand(
        int AuthorId,
        IReadOnlyCollection<BookUpdateRequest> Books
) : IRequest<IReadOnlyCollection<BookDto>>;