using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Responses;
using Newtonsoft.Json;
using Refit;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service
{
    public class AudioService : IAudioService
    {
        private readonly IAudioServiceApi _client;

        public AudioService(string urlBase)
        {
            _client = RestService.For<IAudioServiceApi>(urlBase);
        }

        public byte[] LerBytesAudio(Stream stream)
        {
            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mStream = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mStream.Write(byteArray, 0, bit);
                }
                return mStream.ToArray();
            }
        }

        public async Task<AdicionarAudioResponse> AdicionarAudio(AdicionarAudioCommand command)
        {
            HttpContent response = await _client.AdicionarAudio(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            AdicionarAudioResponse resposta = JsonConvert.DeserializeObject<AdicionarAudioResponse>(json);
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
