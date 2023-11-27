using CRUD.Domain.Domain;
using CRUD.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CRUD.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;
        private string URLAPI = "";


        //Construtor da Controller
        public HomeController(ILogger<HomeController> logger)
        {
            URLAPI = "https://localhost:2985/api/Pessoa";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(URLAPI);
            _logger = logger;
        }

        //Método para retornar a página com os itens retornados pela API
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(URLAPI + "/Listar");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var pessoas = JsonConvert.DeserializeObject<List<Pessoa>>(content);

                return View(pessoas);
            }
            return View();
        }

        //Método para obter um objeto pessoa caso a API encontre

        public async Task<IActionResult> CadastrarEditar(Guid? id)
        {

            if (id.HasValue)
            {
                var response = await _httpClient.GetAsync(URLAPI + "/Obter/" + id);


                if (response.IsSuccessStatusCode)
                {
                    var pessoa = JsonConvert.DeserializeObject<Pessoa>(await response.Content.ReadAsStringAsync());

                    return View(pessoa);
                }
                else
                {
                    //Adicionar mensagem de pessoa não encontrada
                    return View(new Pessoa());
                }
            }
            else
            {
                return View(new Pessoa());
            }

        }
        //Método para cadastrar ou editar um objeto pessoa enviando os dados para a API
        [HttpPost]
        public async Task<IActionResult> CadastrarEditar(Guid id, string nome, DateTime dataNascimento, decimal valorRenda, string CPF)
        {

            if (ModelState.IsValid)
            {
                var pessoa = new Pessoa
                {
                    Id = id != Guid.Empty ? id : Guid.Empty,
                    CPF = CPF.Replace(".", "").Replace("-", ""),
                    Nome = nome,
                    DataNascimento = dataNascimento,
                    ValorRenda = valorRenda
                };

                HttpResponseMessage response = new HttpResponseMessage();
                if (pessoa.Id != Guid.Empty)
                {
                    TempData["MensagemSucess"] = $"Registro editado com sucesso!";
                    response = await _httpClient.PostAsJsonAsync(URLAPI + "/Editar", pessoa);
                }
                else
                {
                    TempData["MensagemSucess"] = $"Cadastro realizado com sucesso!";
                    response = await _httpClient.PostAsJsonAsync(URLAPI + "/Cadastrar", pessoa);

                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = $"Erro no servidor! Por favor tente novamente. ";
                    return View(new Pessoa());
                }
            }
            else
            {
                TempData["MensagemErro"] = $"Erro no servidor! Verifique se os dados estão corretos!";
                return View(new Pessoa());
            }

        }
        //método para deletar um objeto pessoa
        [HttpGet]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var response = await _httpClient.PostAsJsonAsync(URLAPI + "/Deletar/", id);

            if (response.IsSuccessStatusCode)
            {
                TempData["MensagemSucess"] = $"Pessoa: {id} foi excluída com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = $"Não foi possível excluir a pessoa: {id}, tente novamente";
                return RedirectToAction("Index");
            }
        }
    }
}