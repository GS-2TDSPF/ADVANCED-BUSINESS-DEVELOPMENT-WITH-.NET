using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class AnaliseIaConfiguration : IEntityTypeConfiguration<AnaliseIa>
{
    public void Configure(EntityTypeBuilder<AnaliseIa> builder)
    {
        builder.ToTable("TB_ANALISE_IA");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("ID_ANALISE").HasDefaultValueSql("SEQ_ANALISE_IA.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(a => a.DsPrompt).HasColumnName("DS_PROMPT").HasMaxLength(2000);
        builder.Property(a => a.DsResposta).HasColumnName("DS_RESPOSTA").IsRequired();
        builder.Property(a => a.DsModeloIa).HasColumnName("DS_MODELO_IA").HasMaxLength(100);
        builder.Property(a => a.NrTokensUsados).HasColumnName("NR_TOKENS_USADOS");
        builder.Property(a => a.DtGeracao).HasColumnName("DT_GERACAO").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.HasOne(a => a.Alerta).WithOne(al => al.AnaliseIa).HasForeignKey<AnaliseIa>("ID_ALERTA").OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex("ID_ALERTA").IsUnique().HasDatabaseName("UK_ANALISE_ALERTA");
    }
}
