using Core.Domain.Enums.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReferenceBook.Domain.AggregatesModel.CurrencyAggregate;

namespace ReferenceBook.Infrastructure.EntityConfigurations
{
    public class CurrencyEntityConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => x.Code);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(e => e.Code).HasConversion(new EnumToStringConverter<CurrencyCode>());
        }
    }
}