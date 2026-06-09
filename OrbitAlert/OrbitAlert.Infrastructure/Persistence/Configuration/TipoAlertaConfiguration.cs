using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class TipoAlertaConfiguration : IEntityTypeConfiguration<TipoAlerta>
{
    public void Configure(EntityTypeBuilder<TipoAlerta> builder)
    {
        builder.ToTable("TB_TIPO_ALERTA");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("ID_TIPO_ALERTA").HasDefaultValueSql("SEQ_TIPO_ALERTA.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(t => t.NmTipo).HasColumnName("NM_TIPO").HasColumnType("VARCHAR2(50)").IsRequired();
        builder.HasIndex(t => t.NmTipo).IsUnique().HasDatabaseName("UK_TIPO_ALERTA_NOME");
        builder.Property(t => t.DsDescricao).HasColumnName("DS_DESCRICAO").HasColumnType("VARCHAR2(300)");
    }
}
