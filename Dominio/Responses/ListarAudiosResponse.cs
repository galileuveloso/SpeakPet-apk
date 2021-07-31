using Dominio.Models.Visualizacao;
using Dominio.Responses.Base;
using System.Collections.Generic;

namespace Dominio.Responses
{
    public class ListarAudiosResponse : BaseResponse
    {
        public IList<ItemListaAudio> Audios { get; set; }
    }
}
