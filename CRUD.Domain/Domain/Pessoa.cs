
namespace CRUD.Domain.Domain
{
    //Classe pessoa para rodar Migration e adicionar ela no banco de dados
    public class Pessoa
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public decimal ValorRenda { get; set; }

        public string CPF { get; set; }


    }
}
