using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface IZonaRiscoService
{
    ZonaRiscoResponse Create(ZonaRiscoRequest request);
    ZonaRiscoResponse? GetById(long id);
    IReadOnlyList<ZonaRiscoResponse> GetAll();
    IReadOnlyList<ZonaRiscoResponse> GetByMunicipio(long idMunicipio);
    ZonaRiscoResponse Update(long id, ZonaRiscoRequest request);
    bool Delete(long id);
}

public class ZonaRiscoService(IZonaRiscoRepository repository, IMunicipioRepository municipioRepository) : IZonaRiscoService
{
    public ZonaRiscoResponse Create(ZonaRiscoRequest request)
    {
        var municipio = municipioRepository.GetById(request.IdMunicipio)
            ?? throw new KeyNotFoundException($"Município com id '{request.IdMunicipio}' não encontrado.");
        var zona = new Domain.Entities.ZonaRisco(request.NmZona, request.DsDescricao, request.NrLatitude, request.NrLongitude, request.NrLimiarAlerta, request.StAtivo, municipio);
        repository.Add(zona);
        return ZonaRiscoResponse.ToDTO(zona);
    }

    public ZonaRiscoResponse? GetById(long id)
    {
        var zona = repository.GetById(id);
        return zona is null ? null : ZonaRiscoResponse.ToDTO(zona);
    }

    public IReadOnlyList<ZonaRiscoResponse> GetAll() =>
        repository.GetAll().Select(ZonaRiscoResponse.ToDTO).ToList();

    public IReadOnlyList<ZonaRiscoResponse> GetByMunicipio(long idMunicipio) =>
        repository.GetByMunicipio(idMunicipio).Select(ZonaRiscoResponse.ToDTO).ToList();

    public ZonaRiscoResponse Update(long id, ZonaRiscoRequest request)
    {
        var zona = repository.GetById(id)
            ?? throw new KeyNotFoundException("Zona de risco não encontrada.");
        var municipio = municipioRepository.GetById(request.IdMunicipio)
            ?? throw new KeyNotFoundException($"Município com id '{request.IdMunicipio}' não encontrado.");
        zona.Transferir(request.NmZona, request.DsDescricao, request.NrLatitude, request.NrLongitude, request.NrLimiarAlerta, request.StAtivo, municipio);
        repository.Update(zona);
        return ZonaRiscoResponse.ToDTO(zona);
    }

    public bool Delete(long id) => repository.Delete(id);
}