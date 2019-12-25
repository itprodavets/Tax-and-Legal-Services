using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReferenceBook.Domain.AggregatesModel.LanguageAggregate;

namespace ReferenceBook.Infrastructure.EntityConfigurations
{
	public class LanguageEntityConfiguration : IEntityTypeConfiguration<Language>
	{
		public void Configure(EntityTypeBuilder<Language> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).IsRequired();
			builder.HasIndex(x => new {x.Name, x.Code}).IsUnique();

			builder.Property(e => e.Code).HasConversion(new EnumToStringConverter<LanguageCode>());
		}
	}
}