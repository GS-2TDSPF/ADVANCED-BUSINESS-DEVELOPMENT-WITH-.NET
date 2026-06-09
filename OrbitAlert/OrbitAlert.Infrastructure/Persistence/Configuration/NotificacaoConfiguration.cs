using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class NotificacaoConfiguration : IEntityTypeConfiguration<Notificacao>
{
    public void Configure(EntityTypeBuilder<Notificacao> builder)
    {
        builder.ToTable("TB_NOTIFICACAO");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasColumnName("ID_NOTIFICACAO").HasDefaultValueSql("SEQ_NOTIFICACAO.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(n => n.TpNotificacao).HasColumnName("TP_NOTIFICACAO").HasConversion<string>().HasColumnType("VARCHAR2(20)").IsRequired();
        builder.Property(n => n.DsTitulo).HasColumnName("DS_TITULO").HasColumnType("VARCHAR2(200)");
        builder.Property(n => n.DsMensagem).HasColumnName("DS_MENSAGEM").HasColumnType("VARCHAR2(2000)");
        builder.Property(n => n.StLida).HasColumnName("ST_LIDA").HasColumnType("CHAR(1)").IsRequired();
        builder.Property(n => n.DtEnvio).HasColumnName("DT_ENVIO").HasColumnType("DATE").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
        builder.HasOne(n => n.Usuario).WithMany(u => u.Notificacoes).HasForeignKey("ID_USUARIO").OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(n => n.Alerta).WithMany(a => a.Notificacoes).HasForeignKey("ID_ALERTA").OnDelete(DeleteBehavior.Cascade);
    }
}
