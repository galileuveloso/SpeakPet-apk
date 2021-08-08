using Dominio.Commands;
using Dominio.Responses;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Api
{
    public interface IReproducaoApiService
    {
        Task<InserirReproducaoResponse> InserirReproducao(InserirReproducaoCommand command);
        Task<DesativarReproducaoResponse> DesativarReproducao(DesativarReproducaoCommand command);
    }

    public interface IReproducaoApi
    {

        [Post("/reproducao/inserir")]
        Task<HttpContent> InserirReproducao([Body] InserirReproducaoCommand command);

        [Post("/reproducao/desativar")]
        Task<HttpContent> DesativarReproducao([Body] DesativarReproducaoCommand command);
    }
}
