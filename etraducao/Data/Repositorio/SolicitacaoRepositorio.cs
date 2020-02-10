using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using etraducao.Models.Entidades;
using etraducao.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace etraducao.Data.Repositorio
{
    public class SolicitacaoRepositorio : ISolicitacaoRepositorio
    {
        private readonly EtraducaoContexto contexto;

        public SolicitacaoRepositorio(EtraducaoContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task Adicionar(Solicitacao solicitacao)
        {
            await contexto.AddAsync(solicitacao);
            await contexto.SaveChangesAsync();
        }

        public async Task Atualizar(Solicitacao solicitacao)
        {
            contexto.Update(solicitacao);
            await contexto.SaveChangesAsync();
        }

        public async Task<Solicitacao> BuscarSolicitacao(int id)
        {
            return await contexto.Solicitacao
                .Include(x => x.Documentos)
                .Include(x => x.Cliente)
                .Include(x => x.Pagamento)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Solicitacao> DetalharSolicitacao(int id)
        {
            return await contexto.Solicitacao.FindAsync(id);
        }

        public async Task<IEnumerable<Solicitacao>> ListarSolicitacoesPaginadas(int page, string search, string status)
        {
            int quantidadeDeItens = 10;

            var solicitacoes = contexto.Solicitacao
                .Include(x => x.Cliente)
                .Include(x => x.Pagamento)
                .AsNoTracking()
                .OrderByDescending(x => x.DataDaSolicitacao);

            var filtro = solicitacoes.Where(x => x.Cliente.Nome.Contains(search));
            if (!string.IsNullOrEmpty(status))
            {
                var escolha = Enum.Parse(typeof(StatusDePagamento), status);
                filtro = filtro.Where(x => x.Pagamento.StatusDePagamento.Equals(escolha));
            }

            var resultado = filtro.Skip((page - 1) * quantidadeDeItens).Take(quantidadeDeItens);

            return await resultado.ToListAsync();
        }

        public async Task<int> QuantidadeDeItensDaBusca(int page, string search, string status)
        {
            var solicitacoes = contexto.Solicitacao
                .Include(x => x.Cliente)
                .Include(x => x.Pagamento)
                .AsNoTracking()
                .OrderByDescending(x => x.DataDaSolicitacao);

            var filtro = solicitacoes.Where(x => x.Cliente.Nome.Contains(search));
            if (!string.IsNullOrEmpty(status))
            {
                var escolha = Enum.Parse(typeof(StatusDePagamento), status);
                filtro = filtro.Where(x => x.Pagamento.StatusDePagamento.Equals(escolha));
            }

            return await filtro.CountAsync();
        }
    }
}
