using Challenge_MOTTU.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge_MOTTU.Data.Mappings
{
    public class PendingMapping : IEntityTypeConfiguration<Pending>
    {
        public void Configure(EntityTypeBuilder<Pending> builder)
        {
            // Nome da tabela e schema
            builder.ToTable("PENDING", "RM554694");

            // Chave primária
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("ID");

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasColumnName("STATUS")
                   .HasConversion<int>(); 

            builder.Property(p => p.UsuarioId)
                   .IsRequired()
                   .HasColumnName("USUARIO_ID");

            builder.Property(p => p.BikeId)
                   .IsRequired()
                   .HasColumnName("BIKE_ID");

            builder.Property(p => p.DataInicio)
                   .IsRequired()
                   .HasColumnName("DATA_INICIO");

            builder.Property(p => p.DataFim)
                   .HasColumnName("DATA_FIM");

            // Relacionamento com Usuario
            builder.HasOne(p => p.Usuario)
                   .WithMany(u => u.Pendings)
                   .HasForeignKey(p => p.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com Bike
            builder.HasOne(p => p.Bike)
                   .WithMany(b => b.Pendings)
                   .HasForeignKey(p => p.BikeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
