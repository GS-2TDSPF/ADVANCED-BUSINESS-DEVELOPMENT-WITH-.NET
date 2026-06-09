using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface IHistoricoAlertaService
{
    HistoricoAlertaResponse Create(HistoricoAlertaRequest request);
    HistoricoAlertaResponse? GetById(long id);
    IReadOnlyList<HistoricoAlertaResponse> GetAll();
    IReadOnlyList<HistoricoAlertaResponse> GetByAlerta(long idAlerta);
    bool Delete(long id);
}

public class HistoricoAlertaService(IHistoricoAlertaRepository repository, IAlertaRepository alertaRepository) : IHistoricoAlertaService
{
    public HistoricoAlertaResponse Create(HistoricoAlertaRequest request)
    {
        var alerta = alertaRepository.GetById(request.IdAlerta)
            ?? throw new KeyNotFoundException($"Alerta com id '{request.IdAlerta}' não encontrado.");
        var historico = new Domain.Entities.HistoricoAlerta(request.StStatusAnt, request.StStatusNovo, request.NrIndiceRisco, request.DsObservacao, request.NmUsuarioMod, alerta);
        repository.Add(historico);
        return HistoricoAlertaResponse.ToDTO(historico);
    }

    public HistoricoAlertaResponse? GetById(long id)
    {
        var historico = repository.GetById(id);
        return historico is null ? null : HistoricoAlertaResponse.ToDTO(historico);
    }

    public IReadOnlyList<HistoricoAlertaResponse> GetAll() =>
        repository.GetAll().Select(HistoricoAlertaResponse.ToDTO).ToList();

    public IReadOnlyList<HistoricoAlertaResponse> GetByAlerta(long idAlerta) =>
        repository.GetByAlerta(idAlerta).Select(HistoricoAlertaResponse.ToDTO).ToList();

    public bool Delete(long id) => repository.Delete(id);
}
