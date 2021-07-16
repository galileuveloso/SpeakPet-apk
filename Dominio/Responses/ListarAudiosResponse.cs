using Dominio.Models;
using Dominio.Responses.Base;
using System.Collections.Generic;

namespace Dominio.Responses
{
    public class ListarAudiosResponse : BaseResponse
    {
        public IEnumerable<AudioModel> Audios { get; set; }
    }
}
