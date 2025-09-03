using Microsoft.AspNetCore.Mvc;
using Senac.GestaoEscolar.Domain.Dtos.Request.Matriculas;
using Senac.GestaoEscolar.Domain.Dtos.Response;
using Senac.GestaoEscolar.Domain.Services.Matriculas;
using System;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculaController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpGet("Aluno/{alunoId}")]
        public async Task<IActionResult> ObterCursosPorAlunoId([FromRoute] long alunoId)
        {
            try
            {
                var response = await _matriculaService.ObterCursosPorAlunoIdAsync(alunoId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Matricular([FromBody] MatricularRequest request)
        {
            try
            {
                await _matriculaService.MatricularAsync(request);
                return Ok(new { Mensagem = "Matrícula realizada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErroResponse { Mensagem = ex.Message });
            }
        }
    }
}