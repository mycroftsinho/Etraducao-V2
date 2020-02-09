using System;

namespace etraducao.Models.Entidades
{
    public class Documento
    {
        protected Documento()
        {
        }

        public Documento(string contentType, byte[] arquivo, int quantidadeDeCaracteres)
        {
            Id = Guid.NewGuid();

            Arquivo = arquivo;
            ContentType = contentType;
            DefinirQuantidadeDeLaudas(quantidadeDeCaracteres);
        }

        public Guid Id { get; private set; }
        
        public int SolicitacaoId { get; private set; }
        
        public string ContentType { get; private set; }

        public byte[] Arquivo { get; private set; }

        public decimal QuantidadeDeLaudas { get; private set; }

        public int QuantidadeDeCaracteres { get; private set; }

        public Solicitacao Solicitacao { get; private set; }

        private void DefinirQuantidadeDeLaudas(int quantidadeDeCaracteres)
        {
            var laudas = quantidadeDeCaracteres / 1000;
            QuantidadeDeLaudas = laudas < 1 ? 1 : laudas;
            QuantidadeDeCaracteres = quantidadeDeCaracteres;
        }

        public void AlterarSolicitacao(Solicitacao solicitacao)
        {
            SolicitacaoId = solicitacao.Id;
            Solicitacao = solicitacao;
        }
    }
}
