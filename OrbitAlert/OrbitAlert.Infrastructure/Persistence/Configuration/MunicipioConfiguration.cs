using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Configuration;

public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
{
    public void Configure(EntityTypeBuilder<Municipio> builder)
    {
        builder.ToTable("TB_MUNICIPIO");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("ID_MUNICIPIO").HasDefaultValueSql("SEQ_MUNICIPIO.NEXTVAL").ValueGeneratedOnAdd();
        builder.Property(m => m.NmMunicipio).HasColumnName("NM_MUNICIPIO").HasColumnType("VARCHAR2(200)").IsRequired();
        builder.Property(m => m.NmEstado).HasColumnName("NM_ESTADO").HasColumnType("VARCHAR2(100)").IsRequired();
        builder.Property(m => m.NrLatitude).HasColumnName("NR_LATITUDE").HasColumnType("NUMBER(10,7)").IsRequired();
        builder.Property(m => m.NrLongitude).HasColumnName("NR_LONGITUDE").HasColumnType("NUMBER(10,7)").IsRequired();
        builder.Property(m => m.NrPopulacao).HasColumnName("NR_POPULACAO").HasColumnType("NUMBER");
        builder.Property(m => m.StAtivo).HasColumnName("ST_ATIVO").HasColumnType("CHAR(1)").IsRequired();
        builder.Property(m => m.DtCadastro).HasColumnName("DT_CADASTRO").HasColumnType("DATE").HasDefaultValueSql("SYSDATE").ValueGeneratedOnAdd();
    }
}
