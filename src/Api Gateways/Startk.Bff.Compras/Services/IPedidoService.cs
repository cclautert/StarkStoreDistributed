using Stark.Core.Communication;
using Startk.Bff.Compras.Models;

namespace Startk.Bff.Compras.Services;

public interface IPedidoService
{
    Task<ResponseResult> FinalizarPedido(PedidoDTO pedido);
    Task<PedidoDTO> ObterUltimoPedido();
    Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId();

    Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
}