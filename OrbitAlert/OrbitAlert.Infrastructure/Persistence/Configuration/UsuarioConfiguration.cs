using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("TB_USUARIO");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("ID_USUARIO").HasDefaultValueSql("SEQ_USUARIO.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(u => u.NmUsuario).HasColumnName("NM_USUARIO").HasColumnType("VARCHAR2(150)").IsRequired();
        builder.Property(u => u.DsEmail).HasColumnName("DS_EMAIL").HasColumnType("VARCHAR2(200)").IsRequired();
        builder.HasIndex(u => u.DsEmail).IsUnique().HasDatabaseName("TB_USUARIO_EMAIL_UN");
        builder.Property(u => u.DsSenhaHash).HasColumnName("DS_SENHA_HASH").HasColumnType("VARCHAR2(500)").IsRequired();
        builder.Property(u => u.TpPerfil).HasColumnName("TP_PERFIL").HasConversion<string>().HasColumnType("VARCHAR2(10)").IsRequired();
        builder.Property(u => u.StAtivo).HasColumnName("ST_ATIVO").HasColumnType("VARCHAR2(1)").IsRequired();
        builder.Property(u => u.DtCadastro).HasColumnName("DT_CADASTRO").HasColumnType("DATE").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
    }
}
