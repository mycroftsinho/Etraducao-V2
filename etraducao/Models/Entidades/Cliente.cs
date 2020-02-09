using System;
using System.Collections.Generic;
using etraducao.Models.ValuesObjects;

namespace etraducao.Models.Entidades
{
    public class Cliente
    {
        protected Cliente()
        {
            Solicitacoes = new List<Solicitacao>();
        }

        public Cliente(string email, string codigo = "")
        {
            Email = email;
            Cpf = new Cpf(codigo);
            Cnpj = new Cnpj(codigo);
            Solicitacoes = new List<Solicitacao>();
        }

        public Guid Id { get; private set; }

        public string IdExterno { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public Cpf Cpf { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string Cep { get; private set; }

        public string Telefone { get; private set; }

        public ICollection<Solicitacao> Solicitacoes { get; private set; }

        public void AdicionarSolicitacoes(Solicitacao solicitacao)
        {
            Solicitacoes.Add(solicitacao);
        }

        public void InformarCodigoPagamento(string id)
        {
            this.IdExterno = id;
        }

        public void RegistrarCliente(string nome, string codigo)
        {
            Nome = nome;
            Cpf = new Cpf(codigo);
            Cnpj = new Cnpj(codigo);
        }

    }
}
