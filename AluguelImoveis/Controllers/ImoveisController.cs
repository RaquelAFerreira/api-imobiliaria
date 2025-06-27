using AluguelImoveis.Models;
using AluguelImoveis.Models.DTOs;
using AluguelImoveis.Services;
using Microsoft.AspNetCore.Mvc;

namespace AluguelImoveis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImoveisController : ControllerBase
    {
        private readonly ImovelService _imovelService;

        public ImoveisController(ImovelService imovelService)
        {
            _imovelService = imovelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetAll()
        {
            var imoveis = await _imovelService.GetAllAsync();
            return Ok(imoveis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Imovel>> GetById(Guid id)
        {
            var imovel = await _imovelService.GetByIdAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }
            return Ok(imovel);
        }

        [HttpPost]
        public async Task<ActionResult<Imovel>> Create([FromBody] ImovelCreateDto imovelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imovel = new Imovel
            {
                Tipo = imovelDto.Tipo,
                Endereco = imovelDto.Endereco,
                ValorLocacao = imovelDto.ValorLocacao,
                Disponivel = imovelDto.Disponivel,
                Codigo = imovelDto.Codigo
            };

            var createdImovel = await _imovelService.CreateAsync(imovel);

            return CreatedAtAction(nameof(GetById), new { id = createdImovel.Id }, createdImovel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Imovel imovel)
        {
            if (id != imovel.Id)
            {
                return BadRequest("ID do imóvel não corresponde");
            }

            try
            {
                await _imovelService.UpdateAsync(imovel);
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
                await _imovelService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
