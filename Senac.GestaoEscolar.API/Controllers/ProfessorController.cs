using Microsoft.AspNetCore.Mvc;
using Senac.GestaoEscolar.Domain.Dtos.Request.Professores;
using Senac.GestaoEscolar.Domain.Dtos.Response;
using Senac.GestaoEscolar.Domain.Services.Professores;
using System;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _professorService;

        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet("Obter_Todos")]
        public async Task<IActionResult> ObterTodosProfessores([FromQuery] int pagina = 1, [FromQuery] int limite = 10)
        {
            try
            {
                var response = await _professorService.ObterTodosProfessores(pagina, limite);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpGet("{id}/Obter_Professor")]
        public async Task<IActionResult> ObterProfessor([FromRoute] long id)
        {
            try
            {
                var response = await _professorService.ObterProfessor(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpPost("Cadastrar_Professor")]
        public async Task<IActionResult> CadastrarProfessor([FromBody] CadastrarProfessorRequest cadastrarProfessorRequest)
        {
            try
            {
                var response = await _professorService.CadastrarProfessor(cadastrarProfessorRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpPatch("{id}/Atualizar_Professor")]
        public async Task<IActionResult> AtualizarProfessor([FromRoute] long id, [FromBody] AtualizarProfessorRequest atualizarProfessorRequest)
        {
            try
            {
                await _professorService.AtualizarProfessor(id, atualizarProfessorRequest);
                return Ok(new { Mensagem = "Professor atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}/Deletar_Professor")]
        public async Task<IActionResult> DeletarProfessor([FromRoute] long id)
        {
            try
            {
                await _professorService.DeletarProfessor(id);
                return Ok(new { Mensagem = "Professor deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }
    }
}