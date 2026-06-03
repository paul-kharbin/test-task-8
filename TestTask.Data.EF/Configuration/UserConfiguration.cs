using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Data.Contract.Model;

namespace TestTask.Data.EF.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<DataUser>
{
    public void Configure(EntityTypeBuilder<DataUser> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Login).IsRequired().HasMaxLength(50);
        builder.HasIndex(p => p.Login).IsUnique();

        builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(100);
    }
}
