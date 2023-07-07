using CRUD.Domain.Domain;
using CRUD.Domain.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICrud.Controllers
{
    [Route("api/Pessoa")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        private readonly ServerContext _serverContext;

        public PessoaController(ServerContext serverContext)
        {
            _serverContext = serverContext;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<List<Pessoa>> Listar()
        {
            var lista = await _serverContext
                .Pessoa
                .ToListAsync();

            return lista;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] Pessoa pessoa)
        {
            try
            {
                await _serverContext.AddAsync(pessoa);
                await _serverContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{id}")]
        public async Task<Pessoa> Obter([FromRoute] Guid id)
        {
            var pessoa = await _serverContext
                .Pessoa
                .FindAsync(id);
            return pessoa;

        }

        [HttpPost]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Pessoa pessoa)
        {
            var pessoaExistente = await _serverContext.Pessoa.FindAsync(pessoa.Id);
            pessoaExistente.Nome = pessoa.Nome;
            pessoaExistente.DataNascimento = pessoa.DataNascimento;
            pessoaExistente.ValorRenda = pessoa.ValorRenda;
            pessoaExistente.CPF = pessoa.CPF;

            await _serverContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPost]
        [Route("Deletar")]
        public async Task<IActionResult> Deletar([FromBody] Guid id)
        {
            var pessoa = await _serverContext
                .Pessoa
                .FindAsync(id);

            if (pessoa != null)
            {
                _serverContext.Remove(pessoa);
                await _serverContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
