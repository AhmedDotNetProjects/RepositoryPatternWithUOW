using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

namespace RepositoryPatternWithUOW.Api.Requests;

public sealed record UpdateBooksRequest(
    IReadOnlyCollection<BookUpdateRequest> Books);