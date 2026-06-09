using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class AlertaConfiguration : IEntityTypeConfiguration<Alerta>
{
    public void Configure(EntityTypeBuilder<Alerta> builder)
    {
        builder.ToTable("TB_ALERTA");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("ID_ALERTA").HasDefaultValueSql("SEQ_ALERTA.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(a => a.NrNivelRisco).HasColumnName("NR_NIVEL_RISCO").HasColumnType("NUMBER(3,0)").IsRequired();
        builder.Property(a => a.StStatus).HasColumnName("ST_STATUS").HasConversion<string>().HasColumnType("VARCHAR2(20)").IsRequired();
        builder.Property(a => a.DsObservacao).HasColumnName("DS_OBSERVACAO").HasColumnType("VARCHAR2(1000)");
        builder.Property(a => a.DtCriacao).HasColumnName("DT_CRIACAO").HasColumnType("DATE").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.Property(a => a.DtFechamento).HasColumnName("DT_FECHAMENTO").HasColumnType("DATE");
        builder.HasOne(a => a.ZonaRisco).WithMany(z => z.Alertas).HasForeignKey("ID_ZONA").OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(a => a.TipoAlerta).WithMany(t => t.Alertas).HasForeignKey("ID_TIPO_ALERTA").OnDelete(DeleteBehavior.Restrict);
    }
}