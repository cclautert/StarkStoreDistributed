using Stark.Core.Communication;
using Stark.WebApp.MVC.Models;

namespace Stark.WebApp.MVC.Services
{
    public interface IClienteService
    {
        Task<EnderecoViewModel> ObterEndereco();
        Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco);
    }
}
