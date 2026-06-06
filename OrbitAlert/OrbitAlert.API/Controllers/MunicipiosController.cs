using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MunicipiosController(IMunicipioService municipioService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(MunicipioResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] MunicipioRequest request)
    {
        var municipio = municipioService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = municipio.Id }, municipio);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MunicipioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(municipioService.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(MunicipioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var municipio = municipioService.GetById(id);
        if (municipio is null) return NotFound($"Município com id '{id}' não encontrado.");
        return Ok(municipio);
    }

    [HttpGet("estado")]
    [ProducesResponseType(typeof(IEnumerable<MunicipioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByEstado([FromQuery] string nmEstado)
        => Ok(municipioService.GetByEstado(nmEstado));

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(MunicipioResponse), StatusCodes.Status200OK)]
    public IActionResult Update(long id, [FromBody] MunicipioRequest request)
        => Ok(municipioService.Update(id, request));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!municipioService.Delete(id)) return NotFound($"Município com id '{id}' não encontrado.");
        return NoContent();
    }
}
