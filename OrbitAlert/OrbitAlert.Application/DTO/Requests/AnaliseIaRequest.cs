using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Requests;

public record AnaliseIaRequest(
    string? DsPrompt,
    string DsResposta,
    string DsModeloIa,
    long? NrTokensUsados,
    Alerta Alerta)
{
    public AnaliseIa ToEntity() => new(DsPrompt, DsResposta, DsModeloIa, NrTokensUsados, Alerta);
}
