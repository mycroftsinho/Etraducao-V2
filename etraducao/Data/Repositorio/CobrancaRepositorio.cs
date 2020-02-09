using System;
using RestSharp;
using Newtonsoft.Json;
using etraducao.Data.Dto;
using System.Threading.Tasks;
using etraducao.Models.Entidades;
using etraducao.Models.Interfaces;
using Microsoft.Extensions.Configuration;

namespace etraducao.Data.Repositorio
{
    public class CobrancaRepositorio : ICobrancaRepositorio
    {
        private IConfiguration configuration;

        public CobrancaRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task CriarCliente(Cliente cliente)
        {
            try
            {
                var requisicao = new RestClient(new Uri("https://www.asaas.com"));

                var rest = $"api/v3/customers";
                var modelo = new
                {
                    name = cliente.Nome,
                    cpfCnpj = cliente.Cpf.Codigo ?? cliente.Cnpj.Codigo

                };
                var request = new RestRequest(rest, Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("access_token", configuration["secret-key"]);
                request.AddJsonBody(JsonConvert.SerializeObject(modelo));

                var response = requisicao.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var objeto = JsonConvert.DeserializeObject<ClienteDto>(response.Content);

                    cliente.InformarCodigoPagamento(objeto.id);
                };
                return Task.CompletedTask;

            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

        public Task CriarCobranca(Solicitacao solicitacao, string formaDePagamento)
        {
            try
            {
                var requisicao = new RestClient(new Uri("https://www.asaas.com"));
                var rest = $"api/v3/payments";

                var dataDeVencimento = DateTime.Now.AddDays(1);
                var modelo = new
                {
                    customer = solicitacao.Cliente.IdExterno,
                    billingType = formaDePagamento,
                    dueDate = $"{dataDeVencimento.Year}-{dataDeVencimento.Month}-{dataDeVencimento.Day}",
                    value = solicitacao.ValorTotal,
                    description = $"Pedido N° {solicitacao.Id}",
                    externalReference = $"{solicitacao.Id}",
                    postalService = false

                };
                var request = new RestRequest(rest, Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("access_token", configuration["secret-key"]);
                request.AddJsonBody(JsonConvert.SerializeObject(modelo));

                var response = requisicao.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var objeto = JsonConvert.DeserializeObject<PagamentoDto>(response.Content);
                    solicitacao.DefinirPagamento(dataDeVencimento.Date, modelo.description, formaDePagamento, objeto.invoiceUrl, objeto.bankSlipUrl);
                };

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }
    }
}
