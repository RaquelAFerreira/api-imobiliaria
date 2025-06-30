using AluguelImoveis.Models;
using AluguelImoveis.Models.DTOs;
using AluguelImoveis.Services;
using Microsoft.AspNetCore.Mvc;

namespace AluguelImoveis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatariosController : ControllerBase
    {
        private readonly ILocatarioService _locatarioService;

        public LocatariosController(ILocatarioService locatarioService)
        {
            _locatarioService = locatarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locatario>>> GetAll()
        {
            var locatarios = await _locatarioService.GetAllAsync();
            return Ok(locatarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Locatario>> GetById(Guid id)
        {
            var locatario = await _locatarioService.GetByIdAsync(id);
            if (locatario == null)
            {
                return NotFound();
            }
            return Ok(locatario);
        }

        [HttpPost]
        public async Task<ActionResult<Locatario>> Create(
            [FromBody] LocatarioCreateDto locatarioDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locatario = new Locatario
            {
                Telefone = locatarioDto.Telefone,
                CPF = locatarioDto.CPF,
                NomeCompleto = locatarioDto.NomeCompleto
            };

            try
            {
                var createdLocatario = await _locatarioService.CreateAsync(locatario);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdLocatario.Id },
                    createdLocatario
                );
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Locatario locatario)
        {
            if (id != locatario.Id)
            {
                return BadRequest("ID do locatário não corresponde");
            }

            try
            {
                await _locatarioService.UpdateAsync(locatario);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _locatarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
