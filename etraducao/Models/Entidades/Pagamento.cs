using System;

namespace etraducao.Models.Entidades
{
    public class Pagamento
    {
        public Pagamento(Solicitacao solicitacao, DateTime dataDeVencimento, decimal valor, string observacao, FormaDePagamento formaDePagamento, string invoiceUrl, string bankSplit)
        {
            Id = Guid.NewGuid();
            Solicitacao = solicitacao;
            SolicitacaoId = solicitacao.Id;

            Valor = valor;
            Observacao = observacao;
            FormaDePagamento = formaDePagamento;
            DataDeVencimento = dataDeVencimento;
            InvoiceUrl = invoiceUrl;
            BankSlipUrl = bankSplit;

            DevePostarEncomenda = false;
            StatusDePagamento = StatusDePagamento.Pendente;
        }

        public Pagamento(Solicitacao solicitacao)
        {
            Id = Guid.NewGuid();
            Solicitacao = solicitacao;
            SolicitacaoId = solicitacao.Id;

            FormaDePagamento = FormaDePagamento.NaoDefinido;
            StatusDePagamento = StatusDePagamento.NaoDefinido;
        }

        protected Pagamento()
        {
        }

        public Guid Id { get; private set;}

        public int SolicitacaoId { get; private set;}

        public FormaDePagamento FormaDePagamento { get; private set;}

        public DateTime DataDeVencimento { get; private set;}

        public decimal Valor { get; private set;}

        public string Observacao { get; private set;}

        public string InvoiceUrl { get; private set; }

        public string BankSlipUrl { get; private set; }

        public bool DevePostarEncomenda { get; private set;}

        public StatusDePagamento StatusDePagamento { get; private set; }

        public Solicitacao Solicitacao { get; private set;}

        public void InformarValores(DateTime dataDeVencimento, FormaDePagamento formaDePagamento, string observacao, string invoiceUrl, string bankSplit)
        {
            Valor = Solicitacao.ValorTotal;
            DataDeVencimento = dataDeVencimento;
            Observacao = observacao;
            InvoiceUrl = invoiceUrl;
            BankSlipUrl = bankSplit;
            FormaDePagamento = formaDePagamento;
            StatusDePagamento = StatusDePagamento.Pendente;
        }
    }
}
