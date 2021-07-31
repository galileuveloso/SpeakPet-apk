using Dominio.Models;

namespace Dominio.Commands
{
    public class AdicionarAudioYouTubeCommand
    {
        public AdicionarAudioYouTubeCommand(AudioYouTubeModel audioYoutube)
        {
            AudioYoutube = audioYoutube;
        }

        public AudioYouTubeModel AudioYoutube { get; set; }
    }
}
