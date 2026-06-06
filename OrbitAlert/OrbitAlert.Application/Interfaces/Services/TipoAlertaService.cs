using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface ITipoAlertaService
{
    TipoAlertaResponse Create(TipoAlertaRequest request);
    TipoAlertaResponse? GetById(long id);
    IReadOnlyList<TipoAlertaResponse> GetAll();
    TipoAlertaResponse Update(long id, TipoAlertaRequest request);
    bool Delete(long id);
}

public class TipoAlertaService(ITipoAlertaRepository repository) : ITipoAlertaService
{
    public TipoAlertaResponse Create(TipoAlertaRequest request)
    {
        var tipo = request.ToEntity();
        repository.Add(tipo);
        return TipoAlertaResponse.ToDTO(tipo);
    }

    public TipoAlertaResponse? GetById(long id)
    {
        var tipo = repository.GetById(id);
        return tipo is null ? null : TipoAlertaResponse.ToDTO(tipo);
    }

    public IReadOnlyList<TipoAlertaResponse> GetAll() =>
        repository.GetAll().Select(TipoAlertaResponse.ToDTO).ToList();

    public TipoAlertaResponse Update(long id, TipoAlertaRequest request)
    {
        var tipo = repository.GetById(id)
            ?? throw new KeyNotFoundException("Tipo de alerta não encontrado.");
        tipo.Transferir(request.NmTipo, request.DsDescricao);
        repository.SaveChanges();
        return TipoAlertaResponse.ToDTO(tipo);
    }

    public bool Delete(long id) => repository.Delete(id);
}
