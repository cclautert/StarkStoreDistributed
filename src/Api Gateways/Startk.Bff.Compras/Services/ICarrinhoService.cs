using Stark.Core.Communication;
using Startk.Bff.Compras.Models;

namespace Startk.Bff.Compras.Services;

public interface ICarrinhoService
{
    Task<CarrinhoDTO> ObterCarrinho();
    Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoDTO produto);
    Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO carrinho);
    Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
    Task<ResponseResult> AplicarVoucherCarrinho(VoucherDTO voucher);
}