using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using etraducao.Models.ViewModel;
using etraducao.Models.Entidades;
using etraducao.Models.Interfaces;

namespace etraducao.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly ILeitorDePdfService leitorDePdf;
        private readonly ILeitorDeImagemService leitorDeImagem;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ISolicitacaoRepositorio solicitacaoRepositorio;

        public DocumentoController(ILeitorDePdfService leitorDePdf, ILeitorDeImagemService leitorDeImagem, IDocumentoRepositorio documentoRepositorio, ISolicitacaoRepositorio solicitacaoRepositorio)
        {
            this.leitorDePdf = leitorDePdf;
            this.leitorDeImagem = leitorDeImagem;
            this.documentoRepositorio = documentoRepositorio;
            this.solicitacaoRepositorio = solicitacaoRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarArquivo(IList<IFormFile> arquivos)
        {
            int totalDeCaracteres = 0;
            int quantidadeDeCaracteres = 0;
            var documentos = new List<Documento>();
            var listaDeDocumentosParaView = new List<DocumentoContadoViewModel>();
            var solicitacao = await solicitacaoRepositorio.BuscarSolicitacao(1);
            try
            {
                foreach (IFormFile source in arquivos)
                {
                    using (MemoryStream output = new MemoryStream())
                    {
                        if (Models.Configuration.Documento.ValidarImagem(source.ContentType))
                        {
                            source.CopyTo(output);
                            quantidadeDeCaracteres = leitorDeImagem.Executar(output);
                            totalDeCaracteres += quantidadeDeCaracteres;
                        }

                        if (Models.Configuration.Documento.ValidarPdf(source.ContentType))
                        {
                            source.CopyTo(output);
                            quantidadeDeCaracteres = leitorDePdf.Executar(source.FileName, output);
                            totalDeCaracteres += quantidadeDeCaracteres;
                        }

                        var documento = new Documento(source.ContentType, output.ToArray(), quantidadeDeCaracteres);
                        var documentoParaView = new DocumentoContadoViewModel() { DocumentoId = documento.Id.ToString(), QuantidadeDeCaracteres = documento.QuantidadeDeCaracteres, NomeDoArquivo = source.FileName };

                        solicitacao.AdicionarDocumento(documento);
                        listaDeDocumentosParaView.Add(documentoParaView);

                        documentos.Add(documento);
                        await documentoRepositorio.Adicionar(documento);
                    }
                }
                return PartialView("_documentos", listaDeDocumentosParaView);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

    }
}