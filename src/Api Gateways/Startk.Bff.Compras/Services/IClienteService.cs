using Startk.Bff.Compras.Models;

namespace Startk.Bff.Compras.Services;

public interface IClienteService
{
    Task<EnderecoDTO> ObterEndereco();
}