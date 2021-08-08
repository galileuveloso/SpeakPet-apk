using Dominio.Commands;
using Dominio.Interfaces.Api;
using Dominio.Responses;
using Newtonsoft.Json;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Api
{
    public class ReproducaoApiService : IReproducaoApiService
    {
        private readonly IReproducaoApi _client;

        public ReproducaoApiService(string urlBase)
        {
            _client = RestService.For<IReproducaoApi>(urlBase);
        }
        public async Task<InserirReproducaoResponse> InserirReproducao(InserirReproducaoCommand command)
        {
            HttpContent response = await _client.InserirReproducao(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            InserirReproducaoResponse resposta = JsonConvert.DeserializeObject<InserirReproducaoResponse>(json);
            return resposta;
        }

        public async Task<DesativarReproducaoResponse> DesativarReproducao(DesativarReproducaoCommand command)
        {
            HttpContent response = await _client.DesativarReproducao(command).ConfigureAwait(false);
            string json = await response.ReadAsStringAsync().ConfigureAwait(false);
            DesativarReproducaoResponse resposta = JsonConvert.DeserializeObject<DesativarReproducaoResponse>(json);
            return resposta;
        }
    }
}
