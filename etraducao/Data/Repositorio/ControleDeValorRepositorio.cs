using System.Threading.Tasks;
using etraducao.Models.Entidades;
using etraducao.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace etraducao.Data.Repositorio
{
    public class ControleDeValorRepositorio : IControleDeValorRepositorio
    {
        private readonly EtraducaoContexto contexto;

        public ControleDeValorRepositorio(EtraducaoContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<ControleDeValores> BuscarControleDeValor()
        {
            return await contexto.ControleDeValores.FirstAsync();
        }
    }
}
