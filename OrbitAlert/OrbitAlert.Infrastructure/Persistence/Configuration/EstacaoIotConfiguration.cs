using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class EstacaoIotConfiguration : IEntityTypeConfiguration<EstacaoIot>
{
    public void Configure(EntityTypeBuilder<EstacaoIot> builder)
    {
        builder.ToTable("TB_ESTACAO_IOT");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("ID_ESTACAO").HasDefaultValueSql("SEQ_ESTACAO_IOT.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(e => e.NmEstacao).HasColumnName("NM_ESTACAO").HasMaxLength(200).IsRequired();
        builder.Property(e => e.DsLocalizacao).HasColumnName("DS_LOCALIZACAO").HasMaxLength(300);
        builder.Property(e => e.StAtivo).HasColumnName("ST_ATIVO").HasMaxLength(1).IsRequired();
        builder.Property(e => e.DtInstalacao).HasColumnName("DT_INSTALACAO").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.HasOne(e => e.ZonaRisco).WithMany(z => z.Estacoes).HasForeignKey("ID_ZONA").OnDelete(DeleteBehavior.Restrict);
    }
}
