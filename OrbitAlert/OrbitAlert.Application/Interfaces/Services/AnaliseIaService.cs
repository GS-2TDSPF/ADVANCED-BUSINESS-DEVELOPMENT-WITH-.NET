using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface IAnaliseIaService
{
    AnaliseIaResponse Create(AnaliseIaRequest request);
    AnaliseIaResponse? GetById(long id);
    AnaliseIaResponse? GetByAlerta(long idAlerta);
    IReadOnlyList<AnaliseIaResponse> GetAll();
    AnaliseIaResponse Update(long id, AnaliseIaRequest request);
    bool Delete(long id);
}

public class AnaliseIaService(IAnaliseIaRepository repository) : IAnaliseIaService
{
    public AnaliseIaResponse Create(AnaliseIaRequest request)
    {
        var analise = request.ToEntity();
        repository.Add(analise);
        return AnaliseIaResponse.ToDTO(analise);
    }

    public AnaliseIaResponse? GetById(long id)
    {
        var analise = repository.GetById(id);
        return analise is null ? null : AnaliseIaResponse.ToDTO(analise);
    }

    public AnaliseIaResponse? GetByAlerta(long idAlerta)
    {
        var analise = repository.GetByAlerta(idAlerta);
        return analise is null ? null : AnaliseIaResponse.ToDTO(analise);
    }

    public IReadOnlyList<AnaliseIaResponse> GetAll() =>
        repository.GetAll().Select(AnaliseIaResponse.ToDTO).ToList();

    public AnaliseIaResponse Update(long id, AnaliseIaRequest request)
    {
        var analise = repository.GetById(id)
            ?? throw new KeyNotFoundException("Análise de IA não encontrada.");
        analise.Transferir(request.DsPrompt, request.DsResposta, request.DsModeloIa, request.NrTokensUsados, request.Alerta);
        repository.SaveChanges();
        return AnaliseIaResponse.ToDTO(analise);
    }

    public bool Delete(long id) => repository.Delete(id);
}
