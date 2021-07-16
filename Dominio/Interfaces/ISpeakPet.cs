using Dominio.Commands;
using Dominio.Responses;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ISpeakPetService
    {
        Task<EfetuarLoginResponse> EfetuarLogin(EfetuarLoginCommand command);
    }

    public interface ISpeakPetApi
    {
        [Post("/usuario/login")]
        Task<HttpContent> EfetuarLogin([Body] EfetuarLoginCommand request);
    }
}
