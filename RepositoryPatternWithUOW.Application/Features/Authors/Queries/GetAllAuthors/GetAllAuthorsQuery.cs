using MediatR;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Queries.GetAllAuthors;

public sealed record GetAllAuthorsQuery() : IRequest<IEnumerable<AuthorDto?>>;