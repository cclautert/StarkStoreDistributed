using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Stark.Core.Communication;
using Stark.WebApp.MVC.Extensions;
using Stark.WebApp.MVC.Models;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace Stark.WebApp.MVC.Services
{
    public class ComprasBffService : Service, IComprasBffService
    {
        private readonly HttpClient _httpClient;

        public ComprasBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ComprasBffUrl);
        }

        #region Carrinho

        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CarrinhoViewModel>(response);
        }
        public async Task<int> ObterQuantidadeCarrinho()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho-quantidade/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<int>(response);
        }
        public async Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel carrinho)
        {
            var itemContent = ObterConteudo(carrinho);

            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonSerializer.Serialize(carrinho);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync("/compras/carrinho/items/", data);

            //var response = await _httpClient.PostAsJsonAsync("/compras/carrinho/items/", content, JsonSerializerOptions.Default);


            //var content = JsonContent.Create(carrinho);
            //var bytesContent = new ByteArrayContent(content.ToBytes());
            //bytesContent.Headers.Add("Content-Type", "application/json; charset=utf-8"); 
            //var response = await _httpClient.PostAsJsonAsync("/compras/carrinho/items/", bytesContent);



            //var content = new MultipartFormDataContent();
            //var file_content = new ByteArrayContent(new StringContent(json).ReadAsByteArrayAsync().Result);
            //file_content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //file_content.Headers.ContentLength = json.Length;
            //content.Add(file_content);
            //var response = await _httpClient.PostAsJsonAsync("/compras/carrinho/items/", content);


            var content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(new StringContent(json).ReadAsByteArrayAsync().Result);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            fileContent.Headers.ContentLength = json.Length;
            content.Add(fileContent);
            var response = await _httpClient.PostAsync("/compras/carrinho/items/", content);

            //var byteArrayContent = new ByteArrayContent(Encoding.UTF8.GetBytes("{\"ProdutoId\":\"7d67df76-2d4e-4a47-a19c-08eb80a9060b\",\"Nome\":null,\"Quantidade\":1,\"Valor\":0,\"Imagem\":null}"));
            //byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //content.Add(byteArrayContent);
            //var response = await _httpClient.PostAsJsonAsync("/compras/carrinho/items/", content);


            //var content = JsonContent.Create(json);
            //var content = new HttpResponseMessage
            //{
            //    Content = new StringContent(json)
            //};
            //content.Content.Headers.Add(@"Content-Length", json.Length.ToString());
            //content.Content.Headers.Add("Content-Type", "json");
            //content.Content.Headers.Add("Host", "/compras/carrinho/items");
            //var response = await _httpClient.PostAsJsonAsync("/compras/carrinho/items/", content);


            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
        public async Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel item)
        {
            var itemContent = ObterConteudo(item);

            var response = await _httpClient.PutAsync($"/compras/carrinho/items/{produtoId}", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
        public async Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/compras/carrinho/items/{produtoId}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
        public async Task<ResponseResult> AplicarVoucherCarrinho(string voucher)
        {
            var itemContent = ObterConteudo(voucher);

            var response = await _httpClient.PostAsync("/compras/carrinho/aplicar-voucher/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        #endregion

        #region Pedido

        public async Task<ResponseResult> FinalizarPedido(PedidoTransacaoViewModel pedidoTransacao)
        {
            var pedidoContent = ObterConteudo(pedidoTransacao);

            var response = await _httpClient.PostAsync("/compras/pedido/", pedidoContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<PedidoViewModel> ObterUltimoPedido()
        {
            var response = await _httpClient.GetAsync("/compras/pedido/ultimo/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PedidoViewModel>(response);
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterListaPorClienteId()
        {
            var response = await _httpClient.GetAsync("/compras/pedido/lista-cliente/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<PedidoViewModel>>(response);
        }

        public PedidoTransacaoViewModel MapearParaPedido(CarrinhoViewModel carrinho, EnderecoViewModel endereco)
        {
            var pedido = new PedidoTransacaoViewModel
            {
                ValorTotal = carrinho.ValorTotal,
                Itens = carrinho.Itens,
                Desconto = carrinho.Desconto,
                VoucherUtilizado = carrinho.VoucherUtilizado,
                VoucherCodigo = carrinho.Voucher?.Codigo
            };

            if (endereco != null)
            {
                pedido.Endereco = new EnderecoViewModel
                {
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                    Bairro = endereco.Bairro,
                    Cep = endereco.Cep,
                    Complemento = endereco.Complemento,
                    Cidade = endereco.Cidade,
                    Estado = endereco.Estado
                };
            }

            return pedido;
        }

        #endregion
    }
}
