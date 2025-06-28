using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoApiPortfolio.DTOs;
using TodoApiPortfolio.Interfaces;
using TodoApiPortfolio.Models;

//  Tradução de todos [ProducesResponseType] 
//  [ProducesResponseType(StatusCodes.Status200OK)] -> Indica que a resposta será bem-sucedida
//  [ProducesResponseType(StatusCodes.Status201Created)] -> Indica que a criação foi bem-sucedida
//  [ProducesResponseType(StatusCodes.Status204NoContent)] -> Indica que a atualização foi bem-sucedida
//  [ProducesResponseType(StatusCodes.Status400BadRequest)] -> Indica que houve um erro de validação ou formatação na requisição
//  [ProducesResponseType(StatusCodes.Status404NotFound)] -> Indica que o recurso solicitado não foi encontrado

// Cria a aplicação web e configura o pipeline de requisições HTTP.
// --- 4. Configuração do Pipeline de Requisições HTTP ---

namespace TodoApiPortfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IMapper _mapper;

        public TarefasController(ITarefaRepository tarefaRepository, IMapper mapper)
        {
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetTarefas()
        {
            var tarefas = await _tarefaRepository.GetAllAsync();
            var tarefasDto = _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
            return Ok(tarefasDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TarefaDto>> GetTarefa(long id)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            var tarefaDto = _mapper.Map<TarefaDto>(tarefa);
            return Ok(tarefaDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TarefaDto>> PostTarefa([FromBody] CriarTarefaDto criarTarefaDto)
        {
            var tarefaNova = _mapper.Map<Tarefa>(criarTarefaDto);
            await _tarefaRepository.AddAsync(tarefaNova);
            await _tarefaRepository.SaveChangesAsync();
            var tarefaParaRetorno = _mapper.Map<TarefaDto>(tarefaNova);
            return CreatedAtAction(nameof(GetTarefa), new { id = tarefaParaRetorno.Id }, tarefaParaRetorno);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTarefa(long id, [FromBody] TarefaDto tarefaDto)
        {
            if (id != tarefaDto.Id)
            {
                return BadRequest("O ID da URL não corresponde ao ID do corpo da tarefa.");
            }
            var tarefaDoBanco = await _tarefaRepository.GetByIdAsync(id);
            if (tarefaDoBanco == null)
            {
                return NotFound();
            }
            _mapper.Map(tarefaDto, tarefaDoBanco);
            _tarefaRepository.Update(tarefaDoBanco);
            await _tarefaRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTarefa(long id)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            _tarefaRepository.Delete(tarefa);
            await _tarefaRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}