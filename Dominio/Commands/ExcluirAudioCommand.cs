namespace Dominio.Commands
{
    public class ExcluirAudioCommand
    {
        public ExcluirAudioCommand(int idAudio)
        {
            IdAudio = idAudio;
        }

        public int IdAudio { get; set; }
    }
}
