using MediatR;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Queries.GetAuthorById;

public sealed record GetAuthorByIdQuery(int Id) : IRequest<AuthorDto?>;