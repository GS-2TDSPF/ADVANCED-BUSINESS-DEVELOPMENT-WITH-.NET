using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class HistoricoAlertaConfiguration : IEntityTypeConfiguration<HistoricoAlerta>
{
    public void Configure(EntityTypeBuilder<HistoricoAlerta> builder)
    {
        builder.ToTable("TB_HISTORICO_ALERTA");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("ID_HISTORICO").HasDefaultValueSql("SEQ_HISTORICO_ALERTA.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(h => h.StStatusAnt).HasColumnName("ST_STATUS_ANT").HasMaxLength(20);
        builder.Property(h => h.StStatusNovo).HasColumnName("ST_STATUS_NOVO").HasMaxLength(20).IsRequired();
        builder.Property(h => h.NrIndiceRisco).HasColumnName("NR_INDICE_RISCO");
        builder.Property(h => h.DsObservacao).HasColumnName("DS_OBSERVACAO").HasMaxLength(500);
        builder.Property(h => h.NmUsuarioMod).HasColumnName("NM_USUARIO_MOD").HasMaxLength(150);
        builder.Property(h => h.DtAlteracao).HasColumnName("DT_ALTERACAO").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.HasOne(h => h.Alerta).WithMany(a => a.Historicos).HasForeignKey("ID_ALERTA").OnDelete(DeleteBehavior.Cascade);
    }
}
