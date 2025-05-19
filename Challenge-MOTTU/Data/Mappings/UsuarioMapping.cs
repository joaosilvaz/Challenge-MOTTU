using Challenge_MOTTU.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge_MOTTU.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIOS", "RM554694"); // Tabela e schema exatos no Oracle

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("NOME");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("EMAIL");

            builder.Property(u => u.Senha)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("SENHA");

            builder.Property(u => u.Id)
                .HasColumnName("ID");
        }
    }
}
