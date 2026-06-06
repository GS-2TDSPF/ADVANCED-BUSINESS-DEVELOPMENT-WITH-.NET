using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.Interfaces.Services;

public interface IAlertaService
{
    AlertaResponse Create(AlertaRequest request);
    AlertaResponse? GetById(long id);
    IReadOnlyList<AlertaResponse> GetAll();
    IReadOnlyList<AlertaResponse> GetByStatus(StatusAlertaEnum status);
    IReadOnlyList<AlertaResponse> GetByMunicipio(long idMunicipio);
    AlertaResponse Update(long id, AlertaRequest request);
    bool Delete(long id);
}

public class AlertaService(IAlertaRepository repository) : IAlertaService
{
    public AlertaResponse Create(AlertaRequest request)
    {
        var alerta = request.ToEntity();
        repository.Add(alerta);
        return AlertaResponse.ToDTO(alerta);
    }

    public AlertaResponse? GetById(long id)
    {
        var alerta = repository.GetById(id);
        return alerta is null ? null : AlertaResponse.ToDTO(alerta);
    }

    public IReadOnlyList<AlertaResponse> GetAll() =>
        repository.GetAll().Select(AlertaResponse.ToDTO).ToList();

    public IReadOnlyList<AlertaResponse> GetByStatus(StatusAlertaEnum status) =>
        repository.GetByStatus(status).Select(AlertaResponse.ToDTO).ToList();

    public IReadOnlyList<AlertaResponse> GetByMunicipio(long idMunicipio) =>
        repository.GetByMunicipio(idMunicipio).Select(AlertaResponse.ToDTO).ToList();

    public AlertaResponse Update(long id, AlertaRequest request)
    {
        var alerta = repository.GetById(id)
                     ?? throw new KeyNotFoundException("Alerta não encontrado.");
        alerta.Transferir(request.NrNivelRisco, request.StStatus, request.DsObservacao, request.DtFechamento, request.ZonaRisco, request.TipoAlerta);
        repository.SaveChanges();
        return AlertaResponse.ToDTO(alerta);
    }

    public bool Delete(long id) => repository.Delete(id);
}