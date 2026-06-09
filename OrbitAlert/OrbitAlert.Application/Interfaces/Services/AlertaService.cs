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

public class AlertaService(IAlertaRepository repository, IZonaRiscoRepository zonaRepository, ITipoAlertaRepository tipoRepository) : IAlertaService
{
    public AlertaResponse Create(AlertaRequest request)
    {
        var zona = zonaRepository.GetById(request.IdZona)
            ?? throw new KeyNotFoundException($"Zona de risco com id '{request.IdZona}' não encontrada.");
        var tipo = tipoRepository.GetById(request.IdTipoAlerta)
            ?? throw new KeyNotFoundException($"Tipo de alerta com id '{request.IdTipoAlerta}' não encontrado.");
        var alerta = new Domain.Entities.Alerta(request.NrNivelRisco, request.StStatus, request.DsObservacao, request.DtFechamento, zona, tipo);
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
        var zona = zonaRepository.GetById(request.IdZona)
            ?? throw new KeyNotFoundException($"Zona de risco com id '{request.IdZona}' não encontrada.");
        var tipo = tipoRepository.GetById(request.IdTipoAlerta)
            ?? throw new KeyNotFoundException($"Tipo de alerta com id '{request.IdTipoAlerta}' não encontrado.");
        alerta.Transferir(request.NrNivelRisco, request.StStatus, request.DsObservacao, request.DtFechamento, zona, tipo);
        repository.Update(alerta);
        return AlertaResponse.ToDTO(alerta);
    }

    public bool Delete(long id) => repository.Delete(id);
}