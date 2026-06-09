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
        builder.Property(h => h.StStatusAnt).HasColumnName("ST_STATUS_ANT").HasColumnType("VARCHAR2(20)");
        builder.Property(h => h.StStatusNovo).HasColumnName("ST_STATUS_NOVO").HasColumnType("VARCHAR2(20)").IsRequired();
        // ✅ SOLUÇÃO: Remover .HasConversion<int?>() - Oracle não suporta
        builder.Property(h => h.NrIndiceRisco)
            .HasColumnName("NR_INDICE_RISCO")
            .HasColumnType("NUMBER(3,0)");
        builder.Property(h => h.DsObservacao).HasColumnName("DS_OBSERVACAO").HasColumnType("VARCHAR2(500)");
        builder.Property(h => h.NmUsuarioMod).HasColumnName("NM_USUARIO_MOD").HasColumnType("VARCHAR2(100)");
        builder.Property(h => h.DtAlteracao).HasColumnName("DT_ALTERACAO").HasColumnType("DATE").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.HasOne(h => h.Alerta).WithMany().HasForeignKey("ID_ALERTA").OnDelete(DeleteBehavior.Restrict);
    }
}