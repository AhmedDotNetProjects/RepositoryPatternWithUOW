using MediatR;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Commands.AddAuthor;

public sealed record AddAuthorCommand(AuthorRequest Author) : IRequest<AuthorDto>;