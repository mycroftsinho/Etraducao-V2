using System;
using System.Linq;
using System.Collections.Generic;

namespace etraducao.Models.Entidades
{
    public class Solicitacao
    {
        protected Solicitacao()
        {
            Documentos = new List<Documento>();
            DataDaEntrega = DateTime.Now.AddDays(7);
        }

        public Solicitacao(TiposDeSolicitacao tipoDeSolicitacao, DateTime dataDaEntrega, string idiomaDestino, string idiomaOrigem, bool apostilaDeHaia)
        {
            Documentos = new List<Documento>();

            IdiomaOrigem = idiomaOrigem;
            DataDaEntrega = dataDaEntrega;
            IdiomaDestino = idiomaDestino;
            TipoDeSolicitacao = tipoDeSolicitacao;
            ApostilaDeHaia = apostilaDeHaia;

            Pagamento = new Pagamento(this);
        }

        public int Id { get; private set; }

        public Guid ClienteId { get; private set; }

        public Guid PagamentoId { get; private set; }

        public string IdiomaDestino { get; private set; }

        public string IdiomaOrigem { get; private set; }

        public decimal ValorTotal { get; private set; }

        public decimal ValorPorLauda { get; private set; }

        public decimal ValorDoReconhecimentoDeFirma { get; private set; }

        public decimal ValorDaApostilaDeHaia { get; private set; }

        public DateTime DataDaEntrega { get; private set; }

        public decimal QuantidadeDeLaudas { get; private set; }

        public bool ApostilaDeHaia { get; private set; }

        public int QuantidadeDeDocumentosParaApostilar { get; private set; }

        public int QuantidadeDeDocumentosParaReconhecerFirma { get; private set; }

        public TiposDeSolicitacao TipoDeSolicitacao { get; private set; }

        public Cliente Cliente { get; private set; }

        public Pagamento Pagamento { get; private set; }

        public ICollection<Documento> Documentos { get; private set; }

        public void CalcularSolicitacao(ControleDeValores controleDeValor)
        {
            QuantidadeDeLaudas = QuantidadeDeLaudas > 0 ? QuantidadeDeLaudas : Documentos.Sum(x => x.QuantidadeDeLaudas);
            QuantidadeDeDocumentosParaApostilar = QuantidadeDeDocumentosParaApostilar > 0 ? QuantidadeDeDocumentosParaApostilar : Documentos.Count;
            QuantidadeDeDocumentosParaReconhecerFirma = QuantidadeDeDocumentosParaReconhecerFirma > 0 ? QuantidadeDeDocumentosParaReconhecerFirma : Documentos.Count;

            switch (TipoDeSolicitacao)
            {
                case TiposDeSolicitacao.TraducaoTecnica:
                    CalcularTraducaoTecnica(controleDeValor);
                    break;
                case TiposDeSolicitacao.TraducaoJuramentada:
                    CalcularTraducaoJuramentada(controleDeValor);
                    break;
            }
        }

        public void RealizarSemDocumento(int quantidadeDeCaracteres, ControleDeValores controleDeValor)
        {
            QuantidadeDeLaudas = QuantidadeDeLaudas > 0 ? QuantidadeDeLaudas : quantidadeDeCaracteres / 1000m;
            QuantidadeDeDocumentosParaApostilar = 1;
            QuantidadeDeDocumentosParaReconhecerFirma = 1;
            CalcularSolicitacao(controleDeValor);
        }

        private void CalcularTraducaoTecnica(ControleDeValores controleDeValor)
        {
            var totalDeDias = DataDaEntrega.Date.Subtract(DateTime.Now.Date).TotalDays;
            controleDeValor.DefinirQuantidadeDeLaudas(QuantidadeDeLaudas, totalDeDias);
            decimal valorPorLauda = controleDeValor.CalcularValorPorLauda();

            QuantidadeDeLaudas = controleDeValor.QuantidadeDeLaudas;
            ValorPorLauda = valorPorLauda;
            ValorTotal = valorPorLauda * QuantidadeDeLaudas;
        }

        private void CalcularTraducaoJuramentada(ControleDeValores controleDeValor)
        {
            var totalDeDias = DataDaEntrega.Date.Subtract(DateTime.Now.Date).TotalDays;
            controleDeValor.DefinirQuantidadeDeLaudas(QuantidadeDeLaudas, totalDeDias);
            decimal valorPorLauda = controleDeValor.CalcularValorPorLauda();

            QuantidadeDeLaudas = controleDeValor.QuantidadeDeLaudas;

            CalcularValorDeApostilasDeHaia();
            CalcularValorDeReconhecimentoDeFirma();

            ValorPorLauda = valorPorLauda;
            ValorTotal = (valorPorLauda * QuantidadeDeLaudas) + ValorDoReconhecimentoDeFirma + ValorDaApostilaDeHaia;
        }

        private void CalcularValorDeApostilasDeHaia()
        {
            if (ApostilaDeHaia)
            {
                if (QuantidadeDeDocumentosParaApostilar < 5)
                    ValorDaApostilaDeHaia = QuantidadeDeDocumentosParaApostilar * 65m;
                else if (QuantidadeDeDocumentosParaApostilar < 40)
                    ValorDaApostilaDeHaia = QuantidadeDeDocumentosParaApostilar * 60m;
                else
                    ValorDaApostilaDeHaia = QuantidadeDeDocumentosParaApostilar * 55m;
            }
        }

        private void CalcularValorDeReconhecimentoDeFirma()
        {
            ValorDoReconhecimentoDeFirma = QuantidadeDeDocumentosParaReconhecerFirma * 8;
        }

        public void AdicionarDocumento(Documento documento)
        {
            Documentos.Add(documento);
            documento.AlterarSolicitacao(this);
        }


        public void DefinirPagamento(DateTime dataDeVencimento, string observacao, string formaDePagamento, string invoiceUrl, string bankSplit)
        {
            var tipo = formaDePagamento.Equals("BOLETO") ? FormaDePagamento.Boleto : FormaDePagamento.Credit_card;
            Pagamento.InformarValores(dataDeVencimento, tipo, observacao, invoiceUrl, bankSplit);
        }

        public void DefinirCliente(Cliente cliente)
        {
            Cliente = cliente;
            ClienteId = cliente.Id;
        }

        public bool PodeSerSalva()
        {
            return QuantidadeDeLaudas >= 2;
        }

        public void AlterarQuantidadeDeDocumentosParaApostilar(int quantidadeDeDocumentos)
        {
            QuantidadeDeDocumentosParaApostilar = quantidadeDeDocumentos;
        }

        public void AlterarQuantidadeDeDocumentosParaReconhecerFirma(int quantidadeDeDocumentos)
        {
            QuantidadeDeDocumentosParaReconhecerFirma = quantidadeDeDocumentos;
        }
    }
}
