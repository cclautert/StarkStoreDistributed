using Stark.Bff.Compras.Models;

namespace Stark.Bff.Compras.Services;

public interface ICatalogoService
{
    Task<ItemProdutoDTO> ObterPorId(Guid id);
    Task<IEnumerable<ItemProdutoDTO>> ObterItens(IEnumerable<Guid> ids);
}