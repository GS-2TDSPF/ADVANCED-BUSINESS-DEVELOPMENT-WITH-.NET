using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Responses;

public record AnaliseIaResponse(
    long Id,
    string? DsPrompt,
    string DsResposta,
    string DsModeloIa,
    long? NrTokensUsados,
    DateTime DtGeracao,
    AlertaResponse Alerta)
{
    public static AnaliseIaResponse ToDTO(AnaliseIa a) => new(
        a.Id, a.DsPrompt, a.DsResposta, a.DsModeloIa, a.NrTokensUsados, a.DtGeracao,
        AlertaResponse.ToDTO(a.Alerta));
}
