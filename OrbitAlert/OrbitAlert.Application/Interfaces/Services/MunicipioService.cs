using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface IMunicipioService
{
    MunicipioResponse Create(MunicipioRequest request);
    MunicipioResponse? GetById(long id);
    IReadOnlyList<MunicipioResponse> GetAll();
    IReadOnlyList<MunicipioResponse> GetByEstado(string nmEstado);
    MunicipioResponse Update(long id, MunicipioRequest request);
    bool Delete(long id);
}

public class MunicipioService(IMunicipioRepository municipioRepository) : IMunicipioService
{
    public MunicipioResponse Create(MunicipioRequest request)
    {
        var municipio = request.ToEntity();
        municipioRepository.Add(municipio);
        return MunicipioResponse.ToDTO(municipio);
    }

    public MunicipioResponse? GetById(long id)
    {
        var municipio = municipioRepository.GetById(id);
        return municipio is null ? null : MunicipioResponse.ToDTO(municipio);
    }

    public IReadOnlyList<MunicipioResponse> GetAll() =>
        municipioRepository.GetAll().Select(MunicipioResponse.ToDTO).ToList();

    public IReadOnlyList<MunicipioResponse> GetByEstado(string nmEstado) =>
        municipioRepository.GetByEstado(nmEstado).Select(MunicipioResponse.ToDTO).ToList();

    public MunicipioResponse Update(long id, MunicipioRequest request)
    {
        var municipio = municipioRepository.GetById(id)
            ?? throw new KeyNotFoundException("Município não encontrado.");
        municipio.Transferir(request.NmMunicipio, request.NmEstado, request.NrLatitude, request.NrLongitude, request.NrPopulacao, request.StAtivo);
        municipioRepository.SaveChanges();
        return MunicipioResponse.ToDTO(municipio);
    }

    public bool Delete(long id) => municipioRepository.Delete(id);
}
