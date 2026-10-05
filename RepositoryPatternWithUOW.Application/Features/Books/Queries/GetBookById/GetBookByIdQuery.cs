using MediatR;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.GetBookById;

public sealed record GetBookByIdQuery(int Id) : IRequest<BookDto?>;