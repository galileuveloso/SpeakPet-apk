using Dominio.Responses;

namespace Dominio.Interfaces
{
    public interface IReproducaoService
    {
        InserirReproducaoResponse InserirReproducao(int idAudio, int idUsuario);
        DesativarReproducaoResponse DesativarReproducao(int? idReproducao);
    }
}
