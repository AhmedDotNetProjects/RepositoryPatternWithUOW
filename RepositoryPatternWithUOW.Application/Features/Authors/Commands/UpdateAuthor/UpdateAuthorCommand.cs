using MediatR;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.UpdateAuthor;

public sealed record UpdateAuthorCommand(
    int Id,
    string Name
) : IRequest<AuthorDto>;