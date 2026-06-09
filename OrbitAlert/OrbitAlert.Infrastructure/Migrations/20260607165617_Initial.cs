using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitAlert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_MUNICIPIO",
                columns: table => new
                {
                    ID_MUNICIPIO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_MUNICIPIO.NEXTVAL"),
                    NM_MUNICIPIO = table.Column<string>(type: "VARCHAR2(200)", nullable: false),
                    NM_ESTADO = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    NR_LATITUDE = table.Column<decimal>(type: "NUMBER(10,7)", nullable: false),
                    NR_LONGITUDE = table.Column<decimal>(type: "NUMBER(10,7)", nullable: false),
                    NR_POPULACAO = table.Column<decimal>(type: "NUMBER(20,0)", nullable: true),
                    ST_ATIVO = table.Column<string>(type: "CHAR(1)", nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MUNICIPIO", x => x.ID_MUNICIPIO);
                });

            migrationBuilder.CreateTable(
                name: "TB_TIPO_ALERTA",
                columns: table => new
                {
                    ID_TIPO_ALERTA = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_TIPO_ALERTA.NEXTVAL"),
                    NM_TIPO = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    DS_DESCRICAO = table.Column<string>(type: "VARCHAR2(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TIPO_ALERTA", x => x.ID_TIPO_ALERTA);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_USUARIO.NEXTVAL"),
                    NM_USUARIO = table.Column<string>(type: "VARCHAR2(150)", nullable: false),
                    DS_EMAIL = table.Column<string>(type: "VARCHAR2(200)", nullable: false),
                    DS_SENHA_HASH = table.Column<string>(type: "VARCHAR2(500)", nullable: false),
                    TP_PERFIL = table.Column<string>(type: "VARCHAR2(10)", nullable: false),
                    ST_ATIVO = table.Column<string>(type: "VARCHAR2(1)", nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "TB_ZONA_RISCO",
                columns: table => new
                {
                    ID_ZONA = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_ZONA_RISCO.NEXTVAL"),
                    NM_ZONA = table.Column<string>(type: "VARCHAR2(200)", nullable: false),
                    DS_DESCRICAO = table.Column<string>(type: "VARCHAR2(500)", nullable: true),
                    NR_LATITUDE = table.Column<decimal>(type: "NUMBER(10,7)", nullable: false),
                    NR_LONGITUDE = table.Column<decimal>(type: "NUMBER(10,7)", nullable: false),
                    NR_LIMIAR_ALERTA = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ST_ATIVO = table.Column<string>(type: "VARCHAR2(1)", nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE"),
                    ID_MUNICIPIO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ZONA_RISCO", x => x.ID_ZONA);
                    table.ForeignKey(
                        name: "FK_TB_ZONA_RISCO_TB_MUNICIPIO_ID_MUNICIPIO",
                        column: x => x.ID_MUNICIPIO,
                        principalTable: "TB_MUNICIPIO",
                        principalColumn: "ID_MUNICIPIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO_MUNICIPIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_MUNICIPIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DT_VINCULO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO_MUNICIPIO", x => new { x.ID_USUARIO, x.ID_MUNICIPIO });
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_MUNICIPIO_TB_MUNICIPIO_ID_MUNICIPIO",
                        column: x => x.ID_MUNICIPIO,
                        principalTable: "TB_MUNICIPIO",
                        principalColumn: "ID_MUNICIPIO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_MUNICIPIO_TB_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "TB_USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ALERTA",
                columns: table => new
                {
                    ID_ALERTA = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_ALERTA.NEXTVAL"),
                    NR_NIVEL_RISCO = table.Column<byte>(type: "NUMBER(3,0)", nullable: false),
                    ST_STATUS = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    DS_OBSERVACAO = table.Column<string>(type: "VARCHAR2(1000)", nullable: true),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE"),
                    DT_FECHAMENTO = table.Column<DateTime>(type: "DATE", nullable: true),
                    ID_ZONA = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_TIPO_ALERTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALERTA", x => x.ID_ALERTA);
                    table.ForeignKey(
                        name: "FK_TB_ALERTA_TB_TIPO_ALERTA_ID_TIPO_ALERTA",
                        column: x => x.ID_TIPO_ALERTA,
                        principalTable: "TB_TIPO_ALERTA",
                        principalColumn: "ID_TIPO_ALERTA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_ALERTA_TB_ZONA_RISCO_ID_ZONA",
                        column: x => x.ID_ZONA,
                        principalTable: "TB_ZONA_RISCO",
                        principalColumn: "ID_ZONA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_ESTACAO_IOT",
                columns: table => new
                {
                    ID_ESTACAO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_ESTACAO_IOT.NEXTVAL"),
                    NM_ESTACAO = table.Column<string>(type: "VARCHAR2(200)", nullable: false),
                    DS_LOCALIZACAO = table.Column<string>(type: "VARCHAR2(300)", nullable: true),
                    ST_ATIVO = table.Column<string>(type: "VARCHAR2(1)", nullable: false),
                    DT_INSTALACAO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE"),
                    ID_ZONA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ESTACAO_IOT", x => x.ID_ESTACAO);
                    table.ForeignKey(
                        name: "FK_TB_ESTACAO_IOT_TB_ZONA_RISCO_ID_ZONA",
                        column: x => x.ID_ZONA,
                        principalTable: "TB_ZONA_RISCO",
                        principalColumn: "ID_ZONA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_ANALISE_IA",
                columns: table => new
                {
                    ID_ANALISE = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_ANALISE_IA.NEXTVAL"),
                    DS_PROMPT = table.Column<string>(type: "VARCHAR2(2000)", nullable: true),
                    DS_RESPOSTA = table.Column<string>(type: "CLOB", nullable: false),
                    DS_MODELO_IA = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    NR_TOKENS_USADOS = table.Column<decimal>(type: "NUMBER(20,0)", nullable: true),
                    DT_GERACAO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE"),
                    ID_ALERTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ANALISE_IA", x => x.ID_ANALISE);
                    table.ForeignKey(
                        name: "FK_TB_ANALISE_IA_TB_ALERTA_ID_ALERTA",
                        column: x => x.ID_ALERTA,
                        principalTable: "TB_ALERTA",
                        principalColumn: "ID_ALERTA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_HISTORICO_ALERTA",
                columns: table => new
                {
                    ID_HISTORICO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_HISTORICO_ALERTA.NEXTVAL"),
                    ST_STATUS_ANT = table.Column<string>(type: "VARCHAR2(20)", nullable: true),
                    ST_STATUS_NOVO = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    NR_INDICE_RISCO = table.Column<bool>(type: "NUMBER(1)", nullable: true),
                    DS_OBSERVACAO = table.Column<string>(type: "VARCHAR2(500)", nullable: true),
                    NM_USUARIO_MOD = table.Column<string>(type: "VARCHAR2(150)", nullable: true),
                    DT_ALTERACAO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE"),
                    ID_ALERTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_HISTORICO_ALERTA", x => x.ID_HISTORICO);
                    table.ForeignKey(
                        name: "FK_TB_HISTORICO_ALERTA_TB_ALERTA_ID_ALERTA",
                        column: x => x.ID_ALERTA,
                        principalTable: "TB_ALERTA",
                        principalColumn: "ID_ALERTA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_NOTIFICACAO",
                columns: table => new
                {
                    ID_NOTIFICACAO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_NOTIFICACAO.NEXTVAL"),
                    TP_NOTIFICACAO = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    DS_TITULO = table.Column<string>(type: "VARCHAR2(200)", nullable: false),
                    DS_MENSAGEM = table.Column<string>(type: "VARCHAR2(2000)", nullable: true),
                    ST_LIDA = table.Column<string>(type: "CHAR(1)", nullable: false),
                    DT_ENVIO = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE"),
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_ALERTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NOTIFICACAO", x => x.ID_NOTIFICACAO);
                    table.ForeignKey(
                        name: "FK_TB_NOTIFICACAO_TB_ALERTA_ID_ALERTA",
                        column: x => x.ID_ALERTA,
                        principalTable: "TB_ALERTA",
                        principalColumn: "ID_ALERTA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_NOTIFICACAO_TB_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "TB_USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_LEITURA_IOT",
                columns: table => new
                {
                    ID_LEITURA = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_LEITURA_IOT.NEXTVAL"),
                    NR_TEMPERATURA = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    NR_UMIDADE = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    NR_CHUVA_MM = table.Column<decimal>(type: "NUMBER(6,2)", nullable: false),
                    NR_INDICE_RISCO = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DT_LEITURA = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE"),
                    ID_ESTACAO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LEITURA_IOT", x => x.ID_LEITURA);
                    table.ForeignKey(
                        name: "FK_TB_LEITURA_IOT_TB_ESTACAO_IOT_ID_ESTACAO",
                        column: x => x.ID_ESTACAO,
                        principalTable: "TB_ESTACAO_IOT",
                        principalColumn: "ID_ESTACAO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALERTA_ID_TIPO_ALERTA",
                table: "TB_ALERTA",
                column: "ID_TIPO_ALERTA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALERTA_ID_ZONA",
                table: "TB_ALERTA",
                column: "ID_ZONA");

            migrationBuilder.CreateIndex(
                name: "UK_ANALISE_ALERTA",
                table: "TB_ANALISE_IA",
                column: "ID_ALERTA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ESTACAO_IOT_ID_ZONA",
                table: "TB_ESTACAO_IOT",
                column: "ID_ZONA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_HISTORICO_ALERTA_ID_ALERTA",
                table: "TB_HISTORICO_ALERTA",
                column: "ID_ALERTA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_LEITURA_IOT_ID_ESTACAO",
                table: "TB_LEITURA_IOT",
                column: "ID_ESTACAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_NOTIFICACAO_ID_ALERTA",
                table: "TB_NOTIFICACAO",
                column: "ID_ALERTA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_NOTIFICACAO_ID_USUARIO",
                table: "TB_NOTIFICACAO",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "UK_TIPO_ALERTA_NOME",
                table: "TB_TIPO_ALERTA",
                column: "NM_TIPO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TB_USUARIO_EMAIL_UN",
                table: "TB_USUARIO",
                column: "DS_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_MUNICIPIO_ID_MUNICIPIO",
                table: "TB_USUARIO_MUNICIPIO",
                column: "ID_MUNICIPIO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ZONA_RISCO_ID_MUNICIPIO",
                table: "TB_ZONA_RISCO",
                column: "ID_MUNICIPIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ANALISE_IA");

            migrationBuilder.DropTable(
                name: "TB_HISTORICO_ALERTA");

            migrationBuilder.DropTable(
                name: "TB_LEITURA_IOT");

            migrationBuilder.DropTable(
                name: "TB_NOTIFICACAO");

            migrationBuilder.DropTable(
                name: "TB_USUARIO_MUNICIPIO");

            migrationBuilder.DropTable(
                name: "TB_ESTACAO_IOT");

            migrationBuilder.DropTable(
                name: "TB_ALERTA");

            migrationBuilder.DropTable(
                name: "TB_USUARIO");

            migrationBuilder.DropTable(
                name: "TB_TIPO_ALERTA");

            migrationBuilder.DropTable(
                name: "TB_ZONA_RISCO");

            migrationBuilder.DropTable(
                name: "TB_MUNICIPIO");
        }
    }
}
