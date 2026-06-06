using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface IEstacaoIotService
{
    EstacaoIotResponse Create(EstacaoIotRequest request);
    EstacaoIotResponse? GetById(long id);
    IReadOnlyList<EstacaoIotResponse> GetAll();
    IReadOnlyList<EstacaoIotResponse> GetByZona(long idZona);
    EstacaoIotResponse Update(long id, EstacaoIotRequest request);
    bool Delete(long id);
}

public class EstacaoIotService(IEstacaoIotRepository repository) : IEstacaoIotService
{
    public EstacaoIotResponse Create(EstacaoIotRequest request)
    {
        var estacao = request.ToEntity();
        repository.Add(estacao);
        return EstacaoIotResponse.ToDTO(estacao);
    }

    public EstacaoIotResponse? GetById(long id)
    {
        var estacao = repository.GetById(id);
        return estacao is null ? null : EstacaoIotResponse.ToDTO(estacao);
    }

    public IReadOnlyList<EstacaoIotResponse> GetAll() =>
        repository.GetAll().Select(EstacaoIotResponse.ToDTO).ToList();

    public IReadOnlyList<EstacaoIotResponse> GetByZona(long idZona) =>
        repository.GetByZona(idZona).Select(EstacaoIotResponse.ToDTO).ToList();

    public EstacaoIotResponse Update(long id, EstacaoIotRequest request)
    {
        var estacao = repository.GetById(id)
            ?? throw new KeyNotFoundException("Estação IoT não encontrada.");
        estacao.Transferir(request.NmEstacao, request.DsLocalizacao, request.StAtivo, request.ZonaRisco);
        repository.SaveChanges();
        return EstacaoIotResponse.ToDTO(estacao);
    }

    public bool Delete(long id) => repository.Delete(id);
}
