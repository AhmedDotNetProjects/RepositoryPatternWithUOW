using MediatR;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.DeleteAuthor;

public sealed record DeleteAuthorCommand(
    int Id
) : IRequest;