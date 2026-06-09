using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface ILeituraIotService
{
    LeituraIotResponse Create(LeituraIotRequest request);
    LeituraIotResponse? GetById(long id);
    IReadOnlyList<LeituraIotResponse> GetAll();
    IReadOnlyList<LeituraIotResponse> GetByEstacao(long idEstacao);
    bool Delete(long id);
}

public class LeituraIotService(ILeituraIotRepository repository, IEstacaoIotRepository estacaoRepository) : ILeituraIotService
{
    public LeituraIotResponse Create(LeituraIotRequest request)
    {
        var estacao = estacaoRepository.GetById(request.IdEstacao)
            ?? throw new KeyNotFoundException($"Estação IoT com id '{request.IdEstacao}' não encontrada.");
        var leitura = new Domain.Entities.LeituraIot(request.NrTemperatura, request.NrUmidade, request.NrChuvaMm, request.NrIndiceRisco, estacao);
        repository.Add(leitura);
        return LeituraIotResponse.ToDTO(leitura);
    }

    public LeituraIotResponse? GetById(long id)
    {
        var leitura = repository.GetById(id);
        return leitura is null ? null : LeituraIotResponse.ToDTO(leitura);
    }

    public IReadOnlyList<LeituraIotResponse> GetAll() =>
        repository.GetAll().Select(LeituraIotResponse.ToDTO).ToList();

    public IReadOnlyList<LeituraIotResponse> GetByEstacao(long idEstacao) =>
        repository.GetByEstacao(idEstacao).Select(LeituraIotResponse.ToDTO).ToList();

    public bool Delete(long id) => repository.Delete(id);
}
