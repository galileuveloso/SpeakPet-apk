using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Responses;
using Newtonsoft.Json;
using Refit;
using System.Threading.Tasks;

namespace Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioApi _client;

        public UsuarioService(string urlBase)
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
