using Microsoft.AspNetCore.Mvc;
using Senac.GestaoEscolar.Domain.Dtos.Request.Professores;
using Senac.GestaoEscolar.Domain.Dtos.Response;
using Senac.GestaoEscolar.Domain.Services.Professoras;
using Microsoft.AspNetCore.Authorization;


namespace Senac.GestaoEscolar.API.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        [Authorize]
        public class ProfessorController : Controller
        {
            private readonly IProfessorService _professorService;
            public ProfessorController(IProfessorService ProfessorService)
            {
                _professorService = ProfessorService;
            }
            [HttpGet("Obter_Todos")]
            public async Task<IActionResult> ObterTodosProfessors()
            {
                try
                {
                    var professorsResponse = await _professorService.ObterTodosProfessores();
                    return Ok(professorsResponse);
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

            [HttpGet("{id}Obter_Professor")]
            public async Task<IActionResult> ObterProfessor([FromRoute] long id)
            {
                try
                {
                    var ProfessorResponse = await _professorService.ObterProfessor(id);
                    if (ProfessorResponse == null)
                    {
                        return NotFound(new ErroResponse { Mensagem = "Professor não encontrado." });
                    }
                    return Ok(ProfessorResponse);
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

            [HttpPost("Cadastrar_Professor")]
            public async Task<IActionResult> CadastrarProfessor([FromBody] CadastrarProfessorRequest cadastrarProfessorRequest)
            {
                try
                {
                    var cadastrarProfessorResponse = await _professorService.CadastrarProfessor(cadastrarProfessorRequest);
                    return Ok(cadastrarProfessorResponse);
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

            [HttpPatch("{id}Atualizar_Professor")]
            public async Task<IActionResult> AtualizarProfessor([FromRoute] long id, [FromBody] AtualizarProfessorRequest atualizarProfessorRequest)
            {
                try
                {
                    await _professorService.AtualizarProfessor(id, atualizarProfessorRequest);
                    return Ok(new { Mensagem = "Professor atualizado com sucesso." });
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

            [HttpDelete("{id}Deletar_Professor")]
            public async Task<IActionResult> DeletarProfessor([FromRoute] long id)
            {
                try
                {
                    await _professorService.DeletarProfessor(id);
                    return Ok(new { Mensagem = "Professor deletado com sucesso." });
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

