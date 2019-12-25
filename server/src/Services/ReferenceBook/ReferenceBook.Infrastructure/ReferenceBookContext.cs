using Microsoft.EntityFrameworkCore;
using ReferenceBook.Domain.AggregatesModel.CountryAggregate;
using ReferenceBook.Domain.AggregatesModel.LanguageAggregate;

namespace ReferenceBook.Infrastructure
{
    public class ReferenceBookContext : DbContext
    {
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;

        public ReferenceBookContext(DbContextOptions<ReferenceBookContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}