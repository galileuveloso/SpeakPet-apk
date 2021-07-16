using Dominio.Interfaces;
using Dominio.Responses;
using Newtonsoft.Json;
using Refit;
using System.Threading.Tasks;

namespace Service
{
    public class AudioService : IAudioService
    {
        private readonly string urlBase = "http://192.168.0.13:60806/";
        private readonly IAudioServiceApi _client;

        public AudioService()
        {
            _client = RestService.For<IAudioServiceApi>(urlBase);
        }

        public async Task<ListarAudiosResponse> ListarAudios(int idUsuaio)
        {
            var response = await _client.ListarAudios(idUsuaio).ConfigureAwait(true);
            //string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            //ListarAudiosResponse resposta = JsonConvert.DeserializeObject<ListarAudiosResponse>(json);
            return new ListarAudiosResponse();
        }
    }
}
