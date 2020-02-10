using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using etraducao.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace etraducao.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        private readonly ISolicitacaoRepositorio solicitacaoRepositorio;

        public AdministradorController(ISolicitacaoRepositorio solicitacaoRepositorio)
        {
            this.solicitacaoRepositorio = solicitacaoRepositorio;
        }

        public async Task<IActionResult> ListarPedidos(int page = 1, string search = "", string status = "")
        {
            await CarregarViewBagsDePaginacao(page, search, status);
            var solicitacoes = await solicitacaoRepositorio.ListarSolicitacoesPaginadas(page, search, status);
            return View(solicitacoes);
        }

        private async Task CarregarViewBagsDePaginacao(int page = 1, string search = "", string status = "")
        {
            int quantidade = await solicitacaoRepositorio.QuantidadeDeItensDaBusca(page, search, status);
            int quantidadeDePaginas = Convert.ToInt32(Math.Ceiling(quantidade / 10m));

            ViewBag.TotalDeItens = quantidade;
            ViewBag.TotalDePaginas = quantidadeDePaginas;
            ViewBag.Page = page;
        }
    }
}