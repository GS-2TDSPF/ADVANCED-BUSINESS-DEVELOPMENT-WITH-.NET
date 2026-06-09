using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Responses;

public record LeituraIotResponse(
    long Id,
    decimal NrTemperatura,
    decimal NrUmidade,
    decimal NrChuvaMm,
    int NrIndiceRisco,
    DateTime DtLeitura,
    EstacaoIotResponse EstacaoIot)
{
    public static LeituraIotResponse ToDTO(LeituraIot l) => new(
        l.Id, l.NrTemperatura, l.NrUmidade, l.NrChuvaMm, l.NrIndiceRisco, l.DtLeitura,
        EstacaoIotResponse.ToDTO(l.EstacaoIot));
}
