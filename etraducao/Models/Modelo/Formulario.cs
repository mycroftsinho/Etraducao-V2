using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace etraducao.Models.Modelo
{
    public class Formulario
    {
        public Formulario()
        {
            PrevisaoDeEntrega = "";
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Por favor, selecione a linguagem de origem do documento")]
        [DisplayName("De")]
        public string Origem { get; set; }

        [Required(ErrorMessage = "Por favor, selecione a linguagem para a tradução")]
        [DisplayName("Para")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "Por favor, selecione o assunto tratado")]
        [DisplayName("Assunto")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Arro ao Prever Entrega")]
        [DisplayName("Previsão de Entrega")]
        public string PrevisaoDeEntrega { get; set; }

        [Required(ErrorMessage = "Arro ao calcular quantidade de palavras")]
        [DisplayName("Quantidade De Caracteres")]
        public int QuantidadeDeCaracteres { get; set; }

        [Required(ErrorMessage = "Por favor, insira um arquivo.")]
        [DisplayName("Nome do Arquivo")]
        public string NomeDoArquivo { get; set; }

        [Required(ErrorMessage = "Por favor, escolha a solução")]
        [ScaffoldColumn(false)]
        public string Solucao { get; set; }

        [Required(ErrorMessage = "Por favor, escolha a solução")]
        [ScaffoldColumn(false)]
        public decimal Valor { get; set; }

        public IFormFile Arquivo { get; set; }
    }
}
