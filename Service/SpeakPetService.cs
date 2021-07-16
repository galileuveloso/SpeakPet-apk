using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Responses;
using Newtonsoft.Json;
using Refit;
using System.Threading.Tasks;

namespace Service
{
    public class SpeakPetService : ISpeakPetService
    {
        private readonly string urlBase = "http://192.168.0.13:60806/";
        private readonly ISpeakPetApi _client;  

        public SpeakPetService()
        {
            _client = RestService.For<ISpeakPetApi>(urlBase);
        }

        public async Task<EfetuarLoginResponse> EfetuarLogin(EfetuarLoginCommand command)
        {
            var response = await _client.EfetuarLogin(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            EfetuarLoginResponse resposta = JsonConvert.DeserializeObject<EfetuarLoginResponse>(json);
            return new EfetuarLoginResponse();
        }
    }
}
