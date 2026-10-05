using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositoryPatternWithUOW.Domain.Entities;

namespace RepositoryPatternWithUOW.Infranstructure.Data.Configruations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
        builder.HasOne(x => x.Author).WithMany(x => x.Books);
    }
}