using System.Threading.Tasks;
using etraducao.Models.Entidades;
using etraducao.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace etraducao.Data.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly EtraducaoContexto contexto;

        public ClienteRepositorio(EtraducaoContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Cliente> BuscarCliente(string codigo, string email)
        {
            return await contexto.Cliente.FirstOrDefaultAsync(x => (x.Cpf.Codigo.Equals(codigo) || x.Cnpj.Codigo.Equals(codigo)) && x.Email.Equals(email));
        }

        public async Task<Cliente> BuscarClientePorEmail(string email)
        {
            return await contexto.Cliente.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }


        public async Task Adicionar(Cliente cliente)
        {
            await contexto.AddAsync(cliente);
            await contexto.SaveChangesAsync();
        }

        public async Task Atualizar(Cliente cliente)
        {
            contexto.Update(cliente);
            await contexto.SaveChangesAsync();
        }
    }
}
