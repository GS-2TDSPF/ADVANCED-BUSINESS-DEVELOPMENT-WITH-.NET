using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosMunicipiosController(IUsuarioMunicipioService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioMunicipioResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] UsuarioMunicipioRequest request)
        => Created(string.Empty, service.Create(request));

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UsuarioMunicipioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("usuario/{idUsuario:long}")]
    [ProducesResponseType(typeof(IEnumerable<UsuarioMunicipioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByUsuario(long idUsuario) => Ok(service.GetByUsuario(idUsuario));

    [HttpGet("municipio/{idMunicipio:long}")]
    [ProducesResponseType(typeof(IEnumerable<UsuarioMunicipioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByMunicipio(long idMunicipio) => Ok(service.GetByMunicipio(idMunicipio));

    [HttpDelete("{idUsuario:long}/{idMunicipio:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(long idUsuario, long idMunicipio)
    {
        if (!service.Delete(idUsuario, idMunicipio))
            return NotFound($"Vínculo usuário {idUsuario} / município {idMunicipio} não encontrado.");
        return NoContent();
    }
}
