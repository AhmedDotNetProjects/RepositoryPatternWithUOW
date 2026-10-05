using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Domain.Entities;
using RepositoryPatternWithUOW.Infranstructure.Data;

namespace RepositoryPatternWithUOW.Infranstructure.Repositories;

public class AuthorRepository(AppDbContext context) : GenericRepository<Author>(context), IAuthorRepository
{
}