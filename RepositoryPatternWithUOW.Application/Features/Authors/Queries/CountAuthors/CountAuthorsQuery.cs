using MediatR;

namespace RepositoryPatternWithUOW.Application.Features.Authors.Queries.CountAuthors;

public sealed record CountAuthorsQuery : IRequest<int>;