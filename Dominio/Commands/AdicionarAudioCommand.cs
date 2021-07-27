using Dominio.Models;
using System.Collections.Generic;

namespace Dominio.Commands
{
    public class AdicionarAudioCommand
    {
        public AdicionarAudioCommand(IList<AudioModel> audios)
        {
            Audios = audios;
        }

        public IList<AudioModel> Audios { get; set; }
    }
}
