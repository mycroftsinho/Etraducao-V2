using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace etraducao.Models.Components
{
    [ViewComponent(Name = "Paginacao")]
    public class Paginacao : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int quantidadeDePaginas, int totalDeItens, int pagina)
        {
            ViewBag.TotalDeItens = totalDeItens;
            ViewBag.TotalDePaginas = quantidadeDePaginas;
            ViewBag.Pagina = pagina;

            if (quantidadeDePaginas == 1)
                return View("~/Views/Shared/Components/_PaginacaoSingular.cshtml");

            return View("~/Views/Shared/Components/_PaginacaoMultipla.cshtml");
        }

    }
}
