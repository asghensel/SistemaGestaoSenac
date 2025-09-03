using Microsoft.AspNetCore.Mvc;
using Senac.GestaoEscolar.Domain.Dtos.Request.Alunos;
using Senac.GestaoEscolar.Domain.Dtos.Response;
using Senac.GestaoEscolar.Domain.Services.Alunos;
using Microsoft.AspNetCore.Authorization;

namespace Senac.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;
        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }
        [HttpGet("Obter_Todos")]
        public async Task<IActionResult> ObterTodosAlunos()
        {
            try
            {
                var alunosResponse = await _alunoService.ObterTodosAlunos();
                return Ok(alunosResponse);
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

        [HttpGet("{id}/Obter_Aluno")]
        public async Task<IActionResult> ObterAluno([FromRoute] long id)
        {
            try
            {
                var alunoResponse = await _alunoService.ObterAluno(id);
                if (alunoResponse == null)
                {
                    return NotFound(new ErroResponse { Mensagem = "Aluno não encontrado." });
                }
                return Ok(alunoResponse);
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

        [HttpPost("Cadastrar_Aluno")]
        public async Task<IActionResult> CadastrarAluno([FromBody] CadastrarAlunoRequest cadastrarAlunoRequest)
        {
            try
            {
                var cadastrarAlunoResponse = await _alunoService.CadastrarAluno(cadastrarAlunoRequest);
                return Ok(cadastrarAlunoResponse);
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

        [HttpPatch("{id}/Atualizar_Aluno")]
        public async Task<IActionResult> AtualizarAluno([FromRoute] long id, [FromBody] AtualizarAlunoRequest atualizarAlunoRequest)
        {
            try
            {
                await _alunoService.AtualizarAluno(id, atualizarAlunoRequest);
                return Ok(new { Mensagem = "Aluno atualizado com sucesso." });
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

        [HttpDelete("{id}/Deletar_Aluno")]
        public async Task<IActionResult> DeletarAluno([FromRoute] long id)
        {
            try
            {
                await _alunoService.DeletarAluno(id);
                return Ok(new { Mensagem = "Aluno deletado com sucesso." });
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
