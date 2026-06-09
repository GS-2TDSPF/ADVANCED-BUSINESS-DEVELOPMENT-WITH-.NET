using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OrbitAlert.Application.DTO.Requests;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OrbitAlert.API.Swagger;

public class SwaggerExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(UsuarioRequest))
        {
            SetPropertyDescription(schema, "nmUsuario", "Nome completo do usuário.");
            SetPropertyDescription(schema, "dsEmail", "E-mail único do usuário.");
            SetPropertyDescription(schema, "dsSenhaHash", "Senha em texto plano — a API encoda com BCrypt.");
            SetPropertyDescription(schema, "tpPerfil", "Perfil do usuário: ADMIN ou GESTOR.");
            SetPropertyDescription(schema, "stAtivo", "Indica se o usuário está ativo: S ou N.");

            schema.Example = new OpenApiObject
            {
                ["nmUsuario"] = new OpenApiString("Gabriel Sbrana"),
                ["dsEmail"] = new OpenApiString("gabriel@orbitalert.com.br"),
                ["dsSenhaHash"] = new OpenApiString("minhasenha123"),
                ["tpPerfil"] = new OpenApiString("ADMIN"),
                ["stAtivo"] = new OpenApiString("S")
            };
            return;
        }

        if (context.Type == typeof(MunicipioRequest))
        {
            SetPropertyDescription(schema, "nmMunicipio", "Nome do município.");
            SetPropertyDescription(schema, "nmEstado", "Nome do estado.");
            SetPropertyDescription(schema, "nrLatitude", "Latitude geográfica.");
            SetPropertyDescription(schema, "nrLongitude", "Longitude geográfica.");
            SetPropertyDescription(schema, "nrPopulacao", "População estimada. Opcional.");
            SetPropertyDescription(schema, "stAtivo", "Indica se o município está ativo: S ou N.");

            schema.Example = new OpenApiObject
            {
                ["nmMunicipio"] = new OpenApiString("Petrópolis"),
                ["nmEstado"] = new OpenApiString("Rio de Janeiro"),
                ["nrLatitude"] = new OpenApiDouble(-22.5053),
                ["nrLongitude"] = new OpenApiDouble(-43.1786),
                ["nrPopulacao"] = new OpenApiLong(306500),
                ["stAtivo"] = new OpenApiString("S")
            };
            return;
        }

        if (context.Type == typeof(ZonaRiscoRequest))
        {
            SetPropertyDescription(schema, "nmZona", "Nome da zona de risco.");
            SetPropertyDescription(schema, "dsDescricao", "Descrição da zona. Opcional.");
            SetPropertyDescription(schema, "nrLatitude", "Latitude do centroide da zona.");
            SetPropertyDescription(schema, "nrLongitude", "Longitude do centroide da zona.");
            SetPropertyDescription(schema, "nrLimiarAlerta", "Índice mínimo para disparar alerta (1 a 5).");
            SetPropertyDescription(schema, "stAtivo", "Indica se a zona está ativa: S ou N.");

            schema.Example = new OpenApiObject
            {
                ["nmZona"] = new OpenApiString("Morro da Oficina"),
                ["dsDescricao"] = new OpenApiString("Encosta com histórico de deslizamentos em período chuvoso."),
                ["nrLatitude"] = new OpenApiDouble(-22.5120),
                ["nrLongitude"] = new OpenApiDouble(-43.1910),
                ["nrLimiarAlerta"] = new OpenApiInteger(3),
                ["stAtivo"] = new OpenApiString("S")
            };
            return;
        }

        if (context.Type == typeof(EstacaoIotRequest))
        {
            SetPropertyDescription(schema, "nmEstacao", "Nome da estação IoT.");
            SetPropertyDescription(schema, "dsLocalizacao", "Descrição da localização física. Opcional.");
            SetPropertyDescription(schema, "stAtivo", "Indica se a estação está ativa: S ou N.");

            schema.Example = new OpenApiObject
            {
                ["nmEstacao"] = new OpenApiString("EST-001 Morro da Oficina"),
                ["dsLocalizacao"] = new OpenApiString("Base da encosta, próximo ao córrego."),
                ["stAtivo"] = new OpenApiString("S")
            };
            return;
        }

        if (context.Type == typeof(LeituraIotRequest))
        {
            SetPropertyDescription(schema, "nrTemperatura", "Temperatura do ar em graus Celsius.");
            SetPropertyDescription(schema, "nrUmidade", "Umidade relativa do ar em % (0 a 100).");
            SetPropertyDescription(schema, "nrChuvaMm", "Precipitação acumulada em milímetros.");
            SetPropertyDescription(schema, "nrIndiceRisco", "Índice de risco calculado (1 a 5).");

            schema.Example = new OpenApiObject
            {
                ["nrTemperatura"] = new OpenApiDouble(21.8),
                ["nrUmidade"] = new OpenApiDouble(88.5),
                ["nrChuvaMm"] = new OpenApiDouble(25.5),
                ["nrIndiceRisco"] = new OpenApiInteger(4)
            };
            return;
        }

        if (context.Type == typeof(TipoAlertaRequest))
        {
            SetPropertyDescription(schema, "nmTipo", "Nome do tipo de alerta. Será convertido para maiúsculas.");
            SetPropertyDescription(schema, "dsDescricao", "Descrição do tipo de alerta. Opcional.");

            schema.Example = new OpenApiObject
            {
                ["nmTipo"] = new OpenApiString("DESLIZAMENTO"),
                ["dsDescricao"] = new OpenApiString("Risco de deslizamento de terra em encostas monitoradas.")
            };
            return;
        }

        if (context.Type == typeof(AlertaRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["nrNivelRisco"] = new OpenApiInteger(4),
                ["stStatus"] = new OpenApiString("ATIVO"),
                ["dsObservacao"] = new OpenApiString("Precipitação acumulada de 80mm nas últimas 6h."),
                ["dtFechamento"] = new OpenApiNull(),
                ["idZona"] = new OpenApiLong(1),         // ← adicionar
                ["idTipoAlerta"] = new OpenApiLong(1)    // ← adicionar
            };
        }

        if (context.Type == typeof(AnaliseIaRequest))
        {
            SetPropertyDescription(schema, "dsPrompt", "Prompt enviado à Claude API. Opcional.");
            SetPropertyDescription(schema, "dsResposta", "Análise retornada pela Claude API.");
            SetPropertyDescription(schema, "dsModeloIa", "Identificador do modelo utilizado.");
            SetPropertyDescription(schema, "nrTokensUsados", "Tokens consumidos na geração. Opcional.");

            schema.Example = new OpenApiObject
            {
                ["dsPrompt"] = new OpenApiString("Analise o risco de deslizamento com base nos dados do Sentinel-1 e leituras IoT."),
                ["dsResposta"] = new OpenApiString("Com base nos dados orbitais e nas leituras das estações, o risco de deslizamento na zona monitorada é ALTO. Recomendo evacuação preventiva."),
                ["dsModeloIa"] = new OpenApiString("claude-sonnet-4-20250514"),
                ["nrTokensUsados"] = new OpenApiLong(842)
            };
            return;
        }

        if (context.Type == typeof(HistoricoAlertaRequest))
        {
            SetPropertyDescription(schema, "stStatusAnt", "Status anterior do alerta. Opcional.");
            SetPropertyDescription(schema, "stStatusNovo", "Novo status após a alteração.");
            SetPropertyDescription(schema, "nrIndiceRisco", "Índice de risco no momento da alteração. Opcional.");
            SetPropertyDescription(schema, "dsObservacao", "Observação sobre a mudança. Opcional.");
            SetPropertyDescription(schema, "nmUsuarioMod", "Nome do usuário que realizou a alteração. Opcional.");

            schema.Example = new OpenApiObject
            {
                ["stStatusAnt"] = new OpenApiString("ATIVO"),
                ["stStatusNovo"] = new OpenApiString("EM_ATENDIMENTO"),
                ["nrIndiceRisco"] = new OpenApiInteger(4),
                ["dsObservacao"] = new OpenApiString("Equipe de defesa civil acionada."),
                ["nmUsuarioMod"] = new OpenApiString("Gabriel Sbrana")
            };
            return;
        }

        if (context.Type == typeof(NotificacaoRequest))
        {
            SetPropertyDescription(schema, "tpNotificacao", "Tipo: ALERTA, URGENTE ou INFO.");
            SetPropertyDescription(schema, "dsTitulo", "Título da notificação.");
            SetPropertyDescription(schema, "dsMensagem", "Corpo da mensagem. Opcional.");
            SetPropertyDescription(schema, "stLida", "Indica se foi lida: S ou N.");

            schema.Example = new OpenApiObject
            {
                ["tpNotificacao"] = new OpenApiString("URGENTE"),
                ["dsTitulo"] = new OpenApiString("Alerta crítico — Morro da Oficina"),
                ["dsMensagem"] = new OpenApiString("Índice de risco atingiu nível 4. Evacuação preventiva recomendada."),
                ["stLida"] = new OpenApiString("N")
            };
        }
    }

    private static void SetPropertyDescription(OpenApiSchema schema, string propertyName, string description)
    {
        if (schema.Properties.TryGetValue(propertyName, out var property))
            property.Description = description;
    }
}