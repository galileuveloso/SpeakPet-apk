using Dominio.Commands;
using Dominio.Interfaces.Api;
using Dominio.Responses;
using Newtonsoft.Json;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Api
{
    public class AudioApiService : IAudioApiService
    {
        private readonly IAudioApi _client;

        public AudioApiService(string urlBase)
        {
            _client = RestService.For<IAudioApi>(urlBase);
        }

        public async Task<AdicionarAudioResponse> AdicionarAudio(AdicionarAudioCommand command)
        {
            HttpContent response = await _client.AdicionarAudio(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            AdicionarAudioResponse resposta = JsonConvert.DeserializeObject<AdicionarAudioResponse>(json);
            return resposta;
        }

        public async Task<AdicionarAudioYouTubeResponse> AdicionarAudioYouTube(AdicionarAudioYouTubeCommand command)
        {
            HttpContent response = await _client.AdicionarAudioYouTube(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            AdicionarAudioYouTubeResponse resposta = JsonConvert.DeserializeObject<AdicionarAudioYouTubeResponse>(json);
            return resposta;
        }

        public async Task<ListarAudiosResponse> ListarAudios(int idUsuaio)
        {
            HttpResponseMessage response = await _client.ListarAudios(idUsuaio).ConfigureAwait(false);
            string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            ListarAudiosResponse resposta = JsonConvert.DeserializeObject<ListarAudiosResponse>(json);
            return resposta;
        }

        public async Task<ExcluirAudioResponse> ExcluirAudio(ExcluirAudioCommand command)
        {
            HttpContent response = await _client.ExcluirAudio(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            ExcluirAudioResponse resposta = JsonConvert.DeserializeObject<ExcluirAudioResponse>(json);
            return resposta;
        }

        public async Task<EditarAudioResponse> EditarAudio(EditarAudioCommand command)
        {
            HttpContent response = await _client.EditarAudio(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            EditarAudioResponse resposta = JsonConvert.DeserializeObject<EditarAudioResponse>(json);
            return resposta;
        }
    }
}
