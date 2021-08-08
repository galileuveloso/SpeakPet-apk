using Dominio.Responses.Base;

namespace Dominio.Responses
{
    public class InserirReproducaoResponse : BaseResponse
    {
        public bool Reproduzindo { get; set; }
        public int? IdReproducao { get; set; }
    }
}
