using Dominio.Commands;
using Dominio.Responses;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Api
{
    public interface IUsuarioApiService
    {
        Task<EfetuarLoginResponse> EfetuarLogin(EfetuarLoginCommand command);
        Task<InserirUsuarioResponse> InserirUsuario(InserirUsuarioCommand command);
    }

    public interface IUsuarioApi
    {
        [Post("/usuario/login")]
        Task<HttpContent> EfetuarLogin([Body] EfetuarLoginCommand request);

        [Post("/usuario/inserir")]
        Task<HttpContent> InserirUsuario([Body] InserirUsuarioCommand request);
    }
}
