namespace Dominio.Commands
{
    public class AdicionarAudioCommand
    {
        public AdicionarAudioCommand(string titulo, byte[] bytes, int idUsuario)
        {
            Titulo = titulo;
            Bytes = bytes;
            IdUsuario = idUsuario;
        }

        public string Titulo { get; set; }
        public byte[] Bytes { get; set; }
        public int IdUsuario { get; set; }
    }
}
