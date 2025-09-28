using Challenge_MOTTU.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge_MOTTU.Data.Mappings
{
    public class BikeMapping : IEntityTypeConfiguration<Bike>
    {
        public void Configure(EntityTypeBuilder<Bike> builder)
        {
            // Nome da tabela e schema
            builder.ToTable("BIKE", "RM554694");

            // Chave primária
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                   .HasColumnName("ID");

            builder.Property(b => b.Modelo)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("MODELO");

            builder.Property(b => b.Placa)
                   .IsRequired()
                   .HasMaxLength(7)
                   .HasColumnName("PLACA");

            builder.Property(b => b.Ano)
                   .IsRequired()
                   .HasColumnName("ANO");

            builder.Property(b => b.Disponivel)
                   .IsRequired()
                   .HasColumnName("DISPONIVEL");

            // Relacionamento 1:N com Pending
            builder.HasMany(b => b.Pendings)
                   .WithOne(p => p.Bike)
                   .HasForeignKey(p => p.BikeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
