using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class LeituraIotConfiguration : IEntityTypeConfiguration<LeituraIot>
{
    public void Configure(EntityTypeBuilder<LeituraIot> builder)
    {
        builder.ToTable("TB_LEITURA_IOT");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasColumnName("ID_LEITURA").HasDefaultValueSql("SEQ_LEITURA_IOT.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(l => l.NrTemperatura).HasColumnName("NR_TEMPERATURA").HasColumnType("NUMBER(5,2)").IsRequired();
        builder.Property(l => l.NrUmidade).HasColumnName("NR_UMIDADE").HasColumnType("NUMBER(5,2)").IsRequired();
        builder.Property(l => l.NrChuvaMm).HasColumnName("NR_CHUVA_MM").HasColumnType("NUMBER(6,2)").IsRequired();
        builder.Property(l => l.NrIndiceRisco).HasColumnName("NR_INDICE_RISCO").IsRequired();
        builder.Property(l => l.DtLeitura).HasColumnName("DT_LEITURA").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.HasOne(l => l.EstacaoIot).WithMany(e => e.Leituras).HasForeignKey("ID_ESTACAO").OnDelete(DeleteBehavior.Restrict);
    }
}
