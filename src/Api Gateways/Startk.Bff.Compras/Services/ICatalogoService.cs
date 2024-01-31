using Startk.Bff.Compras.Models;

namespace Startk.Bff.Compras.Services;

public interface ICatalogoService
{
    Task<ItemProdutoDTO> ObterPorId(Guid id);
    Task<IEnumerable<ItemProdutoDTO>> ObterItens(IEnumerable<Guid> ids);
}