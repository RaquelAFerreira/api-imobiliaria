using AluguelImoveis.Models;
using AluguelImoveis.Models.DTOs;
using AluguelImoveis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AluguelImoveis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlugueisController : ControllerBase
    {
        private readonly IAluguelService _aluguelService;

        public AlugueisController(IAluguelService aluguelService)
        {
            _aluguelService = aluguelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alugueis = await _aluguelService.GetAllDetailedAsync();
            return Ok(alugueis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluguel>> GetById(Guid id)
        {
            var aluguel = await _aluguelService.GetByIdAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }
            return Ok(aluguel);
        }

        [HttpPost]
        public async Task<ActionResult<Aluguel>> Create([FromBody] AluguelCreateDto aluguelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aluguel = new Aluguel
            {
                ImovelId = aluguelDto.ImovelId,
                LocatarioId = aluguelDto.LocatarioId,
                DataInicio = aluguelDto.DataInicio,
                DataTermino = aluguelDto.DataTermino
            };

            try
            {
                var createdAluguel = await _aluguelService.CreateAsync(aluguel);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdAluguel.Id },
                    createdAluguel
                );
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return BadRequest("ID do aluguel não corresponde");
            }

            try
            {
                await _aluguelService.UpdateAsync(aluguel);
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
                await _aluguelService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
