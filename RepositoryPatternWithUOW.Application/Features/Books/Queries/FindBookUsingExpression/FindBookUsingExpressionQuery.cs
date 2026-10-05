using System.Linq.Expressions;
using MediatR;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.FindBookUsingExpression;

public sealed record FindBookUsingExpressionQuery(Expression<Func<Book, bool>> Match) : IRequest<BookDto?>;