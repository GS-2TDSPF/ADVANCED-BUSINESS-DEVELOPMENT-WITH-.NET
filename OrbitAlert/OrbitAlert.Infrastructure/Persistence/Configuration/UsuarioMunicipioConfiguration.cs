using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class UsuarioMunicipioConfiguration : IEntityTypeConfiguration<UsuarioMunicipio>
{
    public void Configure(EntityTypeBuilder<UsuarioMunicipio> builder)
    {
        builder.ToTable("TB_USUARIO_MUNICIPIO");
        builder.HasKey(um => new { um.IdUsuario, um.IdMunicipio });
        builder.Property(um => um.IdUsuario).HasColumnName("ID_USUARIO");
        builder.Property(um => um.IdMunicipio).HasColumnName("ID_MUNICIPIO");
        builder.Property(um => um.DtVinculo).HasColumnName("DT_VINCULO").HasColumnType("DATE").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.HasOne(um => um.Usuario).WithMany(u => u.Municipios).HasForeignKey(um => um.IdUsuario).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(um => um.Municipio).WithMany(m => m.Usuarios).HasForeignKey(um => um.IdMunicipio).OnDelete(DeleteBehavior.Cascade);
    }
}
