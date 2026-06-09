namespace OrbitAlert.Application.DTO.Requests;

public record LeituraIotRequest(
    decimal NrTemperatura,
    decimal NrUmidade,
    decimal NrChuvaMm,
    int NrIndiceRisco,
    long IdEstacao);
