using Stark.Pedidos.API.Application.DTO;

namespace Stark.Pedidos.API.Application.Queries;

public interface IPedidoQueries
{
    Task<PedidoDTO> ObterUltimoPedido(Guid clienteId);
    Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId(Guid clienteId);
}