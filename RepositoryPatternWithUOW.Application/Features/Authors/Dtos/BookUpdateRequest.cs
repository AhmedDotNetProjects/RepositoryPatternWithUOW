namespace RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

public sealed record BookUpdateRequest(
    int Id,
    string Title);