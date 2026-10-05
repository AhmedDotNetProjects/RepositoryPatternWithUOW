using MediatR;
using RepositoryPatternWithUOW.Application.Features.Books.Dtos;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Application.Features.Books.Queries.GetAllBooks;

public sealed record GetAllBooksQuery() : IRequest<IEnumerable<BookDto>?>;