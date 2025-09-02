using Microsoft.AspNetCore.Mvc;
using Senac.GestaoEscolar.Domain.Dtos.Request.Cursos;
using Senac.GestaoEscolar.Domain.Dtos.Response;
using Senac.GestaoEscolar.Domain.Services.Cursos;
using System;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet("Obter_Todos")]
        public async Task<IActionResult> ObterTodosCursos([FromQuery] int pagina = 1, [FromQuery] int limite = 10)
        {
            try
            {
                // Garante que os valores de paginação sejam válidos
                if (pagina < 1) pagina = 1;
                if (limite < 1) limite = 10;

                var response = await _cursoService.ObterTodosCursos(pagina, limite);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpGet("{id}/Obter_Curso")]
        public async Task<IActionResult> ObterCurso([FromRoute] long id)
        {
            try
            {
                var response = await _cursoService.ObterCurso(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpPost("Cadastrar_Curso")]
        public async Task<IActionResult> CadastrarCurso([FromBody] CadastrarCursoRequest cadastrarCursoRequest)
        {
            try
            {
                var response = await _cursoService.CadastrarCurso(cadastrarCursoRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpPatch("{id}/Atualizar_Curso")]
        public async Task<IActionResult> AtualizarCurso([FromRoute] long id, [FromBody] AtualizarCursoRequest atualizarCursoRequest)
        {
            try
            {
                await _cursoService.AtualizarCurso(id, atualizarCursoRequest);
                return Ok(new { Mensagem = "Curso atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}/Deletar_Curso")]
        public async Task<IActionResult> DeletarCurso([FromRoute] long id)
        {
            try
            {
                await _cursoService.DeletarCurso(id);
                return Ok(new { Mensagem = "Curso deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }
    }
}