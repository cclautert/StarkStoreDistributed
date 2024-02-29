using Stark.Bff.Compras.Models;

namespace Stark.Bff.Compras.Services;

public interface IClienteService
{
    Task<EnderecoDTO> ObterEndereco();
}