using MediatR;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.AddAuthors;

public sealed record AddAuthorsCommand(IReadOnlyCollection<AuthorRequest> Authors) : IRequest<IReadOnlyCollection<AuthorDto>>;