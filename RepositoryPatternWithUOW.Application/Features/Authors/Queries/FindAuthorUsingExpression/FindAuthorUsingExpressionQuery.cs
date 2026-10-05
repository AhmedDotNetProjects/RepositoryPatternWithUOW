using System.Linq.Expressions;
using MediatR;
using RepositoryPatternWithUOW.Application.Features.Authors.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Queries.FindAuthorUsingExpression;

public sealed record FindAuthorUsingExpressionQuery(Expression<Func<Author, bool>> Match) : IRequest<AuthorDto?>;