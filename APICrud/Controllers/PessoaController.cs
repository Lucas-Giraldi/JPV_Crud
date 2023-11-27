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
        //API de pesso
        private readonly ServerContext _serverContext;

        public PessoaController(ServerContext serverContext)
        {
            _serverContext = serverContext;
        }

        //Endpoint de listar objetos do banco de dados da tabela Pessoa utilizando o Entity
        [HttpGet]
        [Route("Listar")]
        public async Task<List<Pessoa>> Listar()
        {
            var lista = await _serverContext
                .Pessoa
                .ToListAsync();

            return lista;
        }
        //Endpoint para Cadastrar uma pessoa, dados recebidos pelo body da requisição
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
        //Endpoint para obter um objeto pessoa caso seja encontrado, dados recebidos pela rota
        [HttpGet]
        [Route("Obter/{id}")]
        public async Task<Pessoa> Obter([FromRoute] Guid id)
        {
            var pessoa = await _serverContext
                .Pessoa
                .FindAsync(id);
            return pessoa;

        }
        //Endpoint para editar um objeto pessoa referente ao Id recebido, dados recebidos pelo body da requisição
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

        //Endpoint para deletar um objeto pessoa referente ao ID enviado pelo body da requisição
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
