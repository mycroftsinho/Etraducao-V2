using etraducao.Models.Entidades;

namespace etraducao.Models.ViewModel
{
    public class ListarPrecoDaSolicitacaoViewModel
    {
        public ListarPrecoDaSolicitacaoViewModel()
        {

        }

        public ListarPrecoDaSolicitacaoViewModel(Solicitacao solicitacao)
        {
            SolicitacaoId = solicitacao.Id;
            DataDeEntrega = solicitacao.DataDaEntrega.ToString("dddd, dd MMMM yyyy");
            QuantidadeDeLaudas = solicitacao.QuantidadeDeLaudas;
            ValorPorLauda = solicitacao.ValorPorLauda;
            ValorTotal = solicitacao.ValorTotal;
            TipoDeSolicitacao = solicitacao.TipoDeSolicitacao;
            Cliente = string.IsNullOrWhiteSpace(solicitacao.Cliente.Nome) ? solicitacao.Cliente.Email : $"{solicitacao.Cliente.Nome} - {solicitacao.Cliente.Cpf.Codigo ?? solicitacao.Cliente.Cnpj.Codigo}";
            DestinoTraducao = solicitacao.IdiomaDestino;
            OrigemTraducao = solicitacao.IdiomaOrigem;
            QuantidadeDeCaracteres = solicitacao.QuantidadeDeLaudas * 1000;
            QuantidadeDeDocumentosParaApostilar = solicitacao.QuantidadeDeDocumentosParaApostilar;
            QuantidadeDeDocumentosParaReconhecerFirma = solicitacao.QuantidadeDeDocumentosParaReconhecerFirma;
            ValorApostilaDeHaia = solicitacao.ValorDaApostilaDeHaia;
            ValorReconhecimentoDeFirma = solicitacao.ValorDoReconhecimentoDeFirma;
        }

        public int SolicitacaoId { get; private set; }

        public decimal QuantidadeDeLaudas { get; set; }

        public decimal QuantidadeDeCaracteres { get; set; }

        public string DataDeEntrega { get; set; }

        public string Cliente { get; set; }

        public string OrigemTraducao { get; set; }

        public string DestinoTraducao { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal ValorPorLauda { get; set; }

        public decimal ValorApostilaDeHaia { get; set; }

        public decimal ValorReconhecimentoDeFirma { get; set; }

        public int QuantidadeDeDocumentosParaApostilar { get; set; }

        public int QuantidadeDeDocumentosParaReconhecerFirma { get; set; }

        public TiposDeSolicitacao TipoDeSolicitacao { get; set; }
    }
}
