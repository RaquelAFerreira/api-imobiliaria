using AluguelImoveis.Models;
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
        public async Task<ActionResult<IEnumerable<Imovel>>> Get()
        {
            var imoveis = await _imovelService.GetAllImoveisAsync();
            return Ok(imoveis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Imovel>> GetById(Guid id)
        {
            var imovel = await _imovelService.GetImovelByIdAsync(id);
            if (imovel == null) return NotFound();
            return Ok(imovel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImovelCreateDto imovelDto)
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
                Disponivel = imovelDto.Disponivel
            };

            var createdImovel = await _imovelService.AddImovelAsync(imovel);

            return CreatedAtAction(nameof(GetById), new { id = createdImovel.Id }, createdImovel);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Put(int id, Imovel imovel)
        // {
        //     if (id != imovel.Id) return BadRequest();
        //     await _imovelService.UpdateImovelAsync(imovel);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     await _imovelService.DeleteImovelAsync(id);
        //     return NoContent();
        // }
    }
}