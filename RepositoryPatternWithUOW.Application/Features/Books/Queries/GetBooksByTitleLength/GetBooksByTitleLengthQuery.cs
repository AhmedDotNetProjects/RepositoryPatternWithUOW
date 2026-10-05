using MediatR;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.GetBooksByTitleLength;

public sealed record GetBooksByTitleLengthQuery(
    int TitleLength
) : IRequest<IReadOnlyCollection<BookDto>>;