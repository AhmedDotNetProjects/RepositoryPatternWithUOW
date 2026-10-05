using MediatR;

namespace RepositoryPatternWithUOW.Application.Features.Books.Dtos;

public sealed record BookRequest(string Title, int AuthorId);