using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Data.Contract.Model;

namespace TestTask.Data.EF.Configuration;

internal sealed class CandidateConfiguration : IEntityTypeConfiguration<DataCandidate>
{
    public void Configure(EntityTypeBuilder<DataCandidate> builder)
    {
        builder.ToTable("Candidates");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.FullName).IsRequired().HasMaxLength(150);

        builder.Property(p => p.BirthDate).HasColumnType("date");

        builder.Property(p => p.DesiredSalary).HasPrecision(18, 2);

        builder.Property(p => p.Email).IsRequired().HasMaxLength(150);

        builder.Property(p => p.Position).IsRequired().HasMaxLength(100);

        builder.Property(p => p.ExperienceYears).IsRequired();

        builder.Property(p => p.CreatedAtUtc).HasColumnType("datetime2").HasDefaultValueSql("GETUTCDATE()");
    }
}
