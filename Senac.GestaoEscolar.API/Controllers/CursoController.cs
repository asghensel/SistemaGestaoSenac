using Microsoft.AspNetCore.Mvc;
using Senac.GestaoEscolar.Domain.Dtos.Request.Cursos;
using Senac.GestaoEscolar.Domain.Dtos.Response;
using Senac.GestaoEscolar.Domain.Services.Cursos;
using Microsoft.AspNetCore.Authorization;

namespace Senac.GestaoEscolar.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;
        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }
        [HttpGet("Obter_Todos")]
        public async Task<IActionResult> ObterTodosCursos()
        {
            try
            {
                var cursosResponse = await _cursoService.ObterTodosCursos();
                return Ok(cursosResponse);
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return NotFound(response);
            }
        }

        [HttpGet("{id}Obter_Curso")]
        public async Task<IActionResult> ObterCurso([FromRoute] long id)
        {
            try
            {
                var cursoResponse = await _cursoService.ObterCurso(id);
                if (cursoResponse == null)
                {
                    return NotFound(new ErroResponse { Mensagem = "Curso não encontrado." });
                }
                return Ok(cursoResponse);
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return NotFound(response);
            }
        }

        [HttpPost("Cadastrar_Curso")]
        public async Task<IActionResult> CadastrarCurso([FromBody] CadastrarCursoRequest cadastrarCursoRequest)
        {
            try
            {
                var cadastrarCursoResponse = await _cursoService.CadastrarCurso(cadastrarCursoRequest);
                return Ok(cadastrarCursoResponse);
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpPatch("{id}Atualizar_Curso")]
        public async Task<IActionResult> AtualizarCurso([FromRoute] long id, [FromBody] AtualizarCursoRequest atualizarCursoRequest)
        {
            try
            {
                await _cursoService.AtualizarCurso(id, atualizarCursoRequest);
                return Ok(new { Mensagem = "Curso atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}Deletar_Curso")]
        public async Task<IActionResult> DeletarCurso([FromRoute] long id)
        {
            try
            {
                await _cursoService.DeletarCurso(id);
                return Ok(new { Mensagem = "Curso deletado com sucesso." });
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(response);
            }
        }
    }
}
