using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Requests;

public record LeituraIotRequest(
    double NrTemperatura,
    double NrUmidade,
    double NrChuvaMm,
    int NrIndiceRisco,
    EstacaoIot EstacaoIot)
{
    public LeituraIot ToEntity() => new(NrTemperatura, NrUmidade, NrChuvaMm, NrIndiceRisco, EstacaoIot);
}
