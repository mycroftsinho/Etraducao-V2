using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using etraducao.Models.Entidades;
using System.ComponentModel.DataAnnotations;

namespace etraducao.Models.ViewModel
{
    public class FormularioPrincipalViewModel
    {
        public FormularioPrincipalViewModel()
        {
            QuantidadeDeCaracteres = 2000;
            PrevisaoDeEntrega = "Automático (melhor preço)";
        }

        [DisplayName("Assunto")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Assunto { get; set; }

        [EmailAddress]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Email { get; set; }

        [DisplayName("De:")]
        [Required(ErrorMessage = "Por favor, selecione a linguagem de origem do documento")]
        public string Origem { get; set; }

        [Required(ErrorMessage = "Por favor, selecione a linguagem para a tradução")]
        [DisplayName("Para:")]
        public string Destino { get; set; }

        [DisplayName("Previsão de Entrega")]
        public string PrevisaoDeEntrega { get; set; }


        [DisplayName("Apostila de HAIA")]
        public bool ApostilaDeHaia { get; set; }

        [Required(ErrorMessage = "por favor, Informe a quantide de caracteres ou faça o upload dos arquivos.")]
        [DisplayName("Quantidade De Caracteres")]
        public int QuantidadeDeCaracteres { get; set; }

        [Required(ErrorMessage = "Por favor, Selecione o Tipo de Solicitação")]
        [DisplayName("Tipo de Solicitacao")]
        public TiposDeSolicitacao TipoDeSolicitacao { get; set; }

        public IList<IFormFile> Arquivos { get; set; }

        [ScaffoldColumn(false)]
        public string Documentos { get; set; }
    }
}
