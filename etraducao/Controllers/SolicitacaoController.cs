using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using etraducao.Models.Entidades;
using etraducao.Models.ViewModel;
using etraducao.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace etraducao.Controllers
{
    public class SolicitacaoController : Controller
    {
        private readonly IClienteRepositorio clienteRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ISolicitacaoRepositorio solicitacaoRepositorio;
        private readonly IControleDeValorRepositorio controleDeValorRepositorio;
        private readonly ICobrancaRepositorio cobrancaRepositorio;

        public SolicitacaoController(IClienteRepositorio clienteRepositorio, IDocumentoRepositorio documentoRepositorio, ISolicitacaoRepositorio solicitacaoRepositorio, IControleDeValorRepositorio controleDeValorRepositorio, ICobrancaRepositorio cobrancaRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.solicitacaoRepositorio = solicitacaoRepositorio;
            this.controleDeValorRepositorio = controleDeValorRepositorio;
            this.cobrancaRepositorio = cobrancaRepositorio;
        }

        public IActionResult Realizar()
        {
            CarregarViewBags();
            return View(new FormularioPrincipalViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Realizar(FormularioPrincipalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cliente = await clienteRepositorio.BuscarClientePorEmail(model.Email);
                if (cliente == null)
                {
                    cliente = new Cliente(model.Email);
                }

                var controleDeValor = await controleDeValorRepositorio.BuscarControleDeValor();
                var solicitacao = new Solicitacao(model.TipoDeSolicitacao, Models.Configuration.Data.ConverterData(model.PrevisaoDeEntrega), model.Destino, model.Origem, model.ApostilaDeHaia);

                if (!string.IsNullOrWhiteSpace(model.Documentos))
                {
                    var documentos = model.Documentos.Split(',');
                    foreach (var documento in documentos)
                    {
                        var id = Guid.Parse(documento);
                        var documentoEmBanco = await documentoRepositorio.BuscarDocumento(id);
                        solicitacao.AdicionarDocumento(documentoEmBanco);
                        solicitacao.CalcularSolicitacao(controleDeValor);
                    }
                }
                else
                {
                    solicitacao.RealizarSemDocumento(model.QuantidadeDeCaracteres, controleDeValor);
                }

                if (solicitacao.PodeSerSalva())
                {
                    solicitacao.DefinirCliente(cliente);
                    await solicitacaoRepositorio.Adicionar(solicitacao);
                }

                return RedirectToAction("Finalizar", new { id = solicitacao.Id });
            }
            CarregarViewBags();
            return View(model);
        }

        public async Task<IActionResult> Finalizar(int id)
        {
            var solicitacao = await solicitacaoRepositorio.BuscarSolicitacao(id);
            var modelo = new ListarPrecoDaSolicitacaoViewModel(solicitacao);
            ViewBag.ReturnUrl = $"/Solicitacao/Finalizar/{id}";
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalizar(int solicitacaoId, string pagamento)
        {
            if (solicitacaoId > 0)
            {
                var solicitacao = await solicitacaoRepositorio.BuscarSolicitacao(solicitacaoId);
                if(!string.IsNullOrEmpty(solicitacao.Pagamento?.InvoiceUrl))
                {
                    return RedirectPermanent(solicitacao.Pagamento.InvoiceUrl);
                }

                await cobrancaRepositorio.CriarCobranca(solicitacao, pagamento);
                await solicitacaoRepositorio.Atualizar(solicitacao);
                return RedirectPermanent(solicitacao.Pagamento.InvoiceUrl);
            }

            var modelo = await solicitacaoRepositorio.BuscarSolicitacao(solicitacaoId);
            var model = new ListarPrecoDaSolicitacaoViewModel(modelo);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BuscarDatasDisponiveis(int quantidadeDeCaracteres)
        {
            if (quantidadeDeCaracteres > 0 && quantidadeDeCaracteres >= 2000)
            {
                var controleDeValor = await controleDeValorRepositorio.BuscarControleDeValor();
                var deadlinesugerido = controleDeValor.ObterDeadLineSugerido(quantidadeDeCaracteres / 1000m);
                var lista = new List<string>();

                while (deadlinesugerido >= 2)
                {
                    var conversao = Decimal.ToInt32(deadlinesugerido);
                    var dataSugerida = DateTime.Now.AddDays(conversao);
                    lista.Add(dataSugerida.Date.ToString("dddd, dd MMMM yyyy"));

                    deadlinesugerido--;
                }

                var resultado = lista.Select(x => new { text = x, value = x }).ToList();
                var selectList = new SelectList(resultado, "text", "value");
                return Json(selectList);
            }
            return Json(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarQuantidadeDeDocumentosParaApostilar(int quantidadeDeDocumentos, int solicitacaoId)
        {
            var solicitacao = await solicitacaoRepositorio.BuscarSolicitacao(solicitacaoId);
            if (solicitacao != null || quantidadeDeDocumentos > 0)
            {
                solicitacao.AlterarQuantidadeDeDocumentosParaApostilar(quantidadeDeDocumentos);
                var controleDeValor = await controleDeValorRepositorio.BuscarControleDeValor();
                solicitacao.CalcularSolicitacao(controleDeValor);
                await solicitacaoRepositorio.Atualizar(solicitacao);
                return RedirectToAction("Finalizar", new { id = solicitacao.Id });
            }
            return BadRequest("Erro na solicitação!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarQuantidadeDeDocumentosParaReconhecimentoDeFirma(int documentos, int id)
        {
            var solicitacao = await solicitacaoRepositorio.BuscarSolicitacao(id);
            if (solicitacao != null && documentos > 0)
            {
                solicitacao.AlterarQuantidadeDeDocumentosParaReconhecerFirma(documentos);
                var controleDeValor = await controleDeValorRepositorio.BuscarControleDeValor();
                solicitacao.CalcularSolicitacao(controleDeValor);
                await solicitacaoRepositorio.Atualizar(solicitacao);
                return RedirectToAction("Finalizar", new { id = solicitacao.Id });
            }
            return BadRequest("Erro na solicitação!");
        }

        private void CarregarViewBags()
        {
            string[] listaDeLinguagens = new[] { "Português", "Inglês", "Espanhol", "Francês", "Alemão", "Jordânia", "Italiano" };
            string[] assuntos = new[] { "Biologia", "Contábeis", "Tecnologia", "Direito", "Quimica", "Física" };

            ViewBag.Origens = new SelectList(listaDeLinguagens);
            ViewBag.Destinos = new SelectList(listaDeLinguagens);
            ViewBag.Assuntos = new SelectList(assuntos);

        }
    }
}