using System;
using System.Collections.Generic;

namespace etraducao.Models.Entidades
{
    public class Tradutor
    {
        public Guid Id { get; private set; }

        public string Nome { get; private set; }

        public ICollection<Solicitacao> Solicitacoes { get; private set; }
    }
}
