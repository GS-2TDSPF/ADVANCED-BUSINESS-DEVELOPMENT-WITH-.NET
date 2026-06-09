namespace OrbitAlert.Application.DTO.Requests;

public record AnaliseIaRequest(
    string? DsPrompt,
    string DsResposta,
    string DsModeloIa,
    long? NrTokensUsados,
    long IdAlerta);
