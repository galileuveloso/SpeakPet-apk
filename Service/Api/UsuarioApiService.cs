using Dominio.Commands;
using Dominio.Interfaces.Api;
using Dominio.Responses;
using Newtonsoft.Json;
using Refit;
using System.Threading.Tasks;

namespace Service.Api
{
    public class UsuarioApiService : IUsuarioApiService
    {
        private readonly IUsuarioApi _client;

        public UsuarioApiService(string urlBase)
        {
            _client = RestService.For<IUsuarioApi>(urlBase);
        }

        public async Task<EfetuarLoginResponse> EfetuarLogin(EfetuarLoginCommand command)
        {
            var response = await _client.EfetuarLogin(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            EfetuarLoginResponse resposta = JsonConvert.DeserializeObject<EfetuarLoginResponse>(json);
            return resposta;
        }

        public async Task<InserirUsuarioResponse> InserirUsuario(InserirUsuarioCommand command)
        {
            var response = await _client.InserirUsuario(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            InserirUsuarioResponse resposta = JsonConvert.DeserializeObject<InserirUsuarioResponse>(json);
            return resposta;
        }
    }
}
