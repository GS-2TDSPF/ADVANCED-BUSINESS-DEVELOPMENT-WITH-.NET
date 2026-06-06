using Microsoft.AspNetCore.Mvc;
using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Services;

namespace OrbitAlert.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeiturasIotController(ILeituraIotService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(LeituraIotResponse), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] LeituraIotRequest request)
    {
        var leitura = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = leitura.Id }, leitura);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LeituraIotResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(LeituraIotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var leitura = service.GetById(id);
        if (leitura is null) return NotFound($"Leitura IoT com id '{id}' não encontrada.");
        return Ok(leitura);
    }

    [HttpGet("estacao/{idEstacao:long}")]
    [ProducesResponseType(typeof(IEnumerable<LeituraIotResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByEstacao(long idEstacao) => Ok(service.GetByEstacao(idEstacao));

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(long id)
    {
        if (!service.Delete(id)) return NotFound($"Leitura IoT com id '{id}' não encontrada.");
        return NoContent();
    }
}
