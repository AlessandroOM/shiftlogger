using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Map
{
    public class LoggerMap : IEntityTypeConfiguration<Logger>
    {
        public void Configure(EntityTypeBuilder<Logger> builder)
        {
            builder.ToTable("LOGGER");
            builder.HasKey(m=> m.loggerID);
            builder.HasIndex(m => m.Inicio);
            builder.Property(M => M.Inicio)
                .IsRequired();

            builder.Property(M => M.Fim);
            builder.Property(m => m.Atividade)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}