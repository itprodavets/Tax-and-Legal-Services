using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReferenceBook.Domain.AggregatesModel.CountryAggregate;

namespace ReferenceBook.Infrastructure.EntityConfigurations
{
	public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).IsRequired();
			builder.HasIndex(x => new {x.Name, x.Alpha2Code, x.Alpha3Code}).IsUnique();

			builder.Property(e => e.Alpha2Code).HasConversion(new EnumToStringConverter<Alpha2Code>());
			builder.Property(e => e.Alpha3Code).HasConversion(new EnumToStringConverter<Alpha3Code>());
			builder.Property(e => e.Region).HasConversion(new EnumToStringConverter<Region>());
			builder.Property(e => e.SubRegion).HasConversion(new EnumToStringConverter<SubRegion>());
		}
	}
}