using Stark.Pedidos.API.Application.DTO;

namespace Stark.Pedidos.API.Application.Queries;

public interface IVoucherQueries
{
    Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
}