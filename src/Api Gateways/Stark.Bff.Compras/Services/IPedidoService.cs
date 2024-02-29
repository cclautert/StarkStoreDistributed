using Stark.Bff.Compras.Models;
using Stark.Core.Communication;

namespace Stark.Bff.Compras.Services;

public interface IPedidoService
{
    Task<ResponseResult> FinalizarPedido(PedidoDTO pedido);
    Task<PedidoDTO> ObterUltimoPedido();
    Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId();

    Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
}