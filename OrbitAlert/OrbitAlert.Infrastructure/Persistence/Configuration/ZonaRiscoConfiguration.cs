using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class ZonaRiscoConfiguration : IEntityTypeConfiguration<ZonaRisco>
{
    public void Configure(EntityTypeBuilder<ZonaRisco> builder)
    {
        builder.ToTable("TB_ZONA_RISCO");
        builder.HasKey(z => z.Id);
        builder.Property(z => z.Id).HasColumnName("ID_ZONA").HasDefaultValueSql("SEQ_ZONA_RISCO.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(z => z.NmZona).HasColumnName("NM_ZONA").HasMaxLength(200).IsRequired();
        builder.Property(z => z.DsDescricao).HasColumnName("DS_DESCRICAO").HasMaxLength(500);
        builder.Property(z => z.NrLatitude).HasColumnName("NR_LATITUDE").HasColumnType("NUMBER(10,7)").IsRequired();
        builder.Property(z => z.NrLongitude).HasColumnName("NR_LONGITUDE").HasColumnType("NUMBER(10,7)").IsRequired();
        builder.Property(z => z.NrLimiarAlerta).HasColumnName("NR_LIMIAR_ALERTA").IsRequired();
        builder.Property(z => z.StAtivo).HasColumnName("ST_ATIVO").HasMaxLength(1).IsRequired();
        builder.Property(z => z.DtCadastro).HasColumnName("DT_CADASTRO").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.HasOne(z => z.Municipio).WithMany(m => m.Zonas).HasForeignKey("ID_MUNICIPIO").OnDelete(DeleteBehavior.Restrict);
    }
}
