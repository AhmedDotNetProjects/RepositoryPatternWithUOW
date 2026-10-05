namespace RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

public sealed record AuthorRequest(string Name, IReadOnlyCollection<string> BookTitles);