namespace RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

public sealed record AuthorDto(int AuthorId, string Name, IEnumerable<string> BooksTitles);
