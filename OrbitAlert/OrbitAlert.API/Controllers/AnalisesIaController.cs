using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalisesIaController(IAnaliseIaService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(AnaliseIaResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] AnaliseIaRequest request)
    {
        var analise = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = analise.Id }, analise);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnaliseIaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(AnaliseIaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var analise = service.GetById(id);
        if (analise is null) return NotFound($"Análise de IA com id '{id}' não encontrada.");
        return Ok(analise);
    }

    [HttpGet("alerta/{idAlerta:long}")]
    [ProducesResponseType(typeof(AnaliseIaResponse), StatusCodes.Status200OK)]
    public IActionResult GetByAlerta(long idAlerta) => Ok(service.GetByAlerta(idAlerta));

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(AnaliseIaResponse), StatusCodes.Status200OK)]
    public IActionResult Update(long id, [FromBody] AnaliseIaRequest request) => Ok(service.Update(id, request));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!service.Delete(id)) return NotFound($"Análise de IA com id '{id}' não encontrada.");
        return NoContent();
    }
}
