using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Requests;

public record EstacaoIotRequest(
    string NmEstacao,
    string? DsLocalizacao,
    string StAtivo,
    ZonaRisco ZonaRisco)
{
    public EstacaoIot ToEntity() => new(NmEstacao, DsLocalizacao, StAtivo, ZonaRisco);
}
