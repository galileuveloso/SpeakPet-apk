namespace Dominio.Commands
{
    public class EditarAudioCommand
    {
        public EditarAudioCommand(int idAudio, string novoTitulo)
        {
            IdAudio = idAudio;
            NovoTitulo = novoTitulo;
        }

        public int IdAudio { get; set; }
        public string NovoTitulo { get; set; }
    }
}
