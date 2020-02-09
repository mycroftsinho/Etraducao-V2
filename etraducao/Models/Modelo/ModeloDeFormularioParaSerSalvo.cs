using System.Collections.Generic;

namespace etraducao.Models.Modelo
{
    public class ModeloDeFormularioParaSerSalvo
    {
        public string Origem { get; set; }

        public string Destino { get; set; }

        public string Assunto { get; set; }

        public string PrevisaoDeEntrega { get; set; }

        public int QuantidadeDeCaracteres { get; set; }

        public string NomeDoArquivo { get; set; }

        public string Solucao { get; set; }

        public decimal Valor { get; set; }

        public string ContentType { get; set; }

        public ICollection<Caminho> CaminhoDoArquivo { get; set; }
    }
}
