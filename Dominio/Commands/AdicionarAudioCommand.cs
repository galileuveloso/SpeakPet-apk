namespace Dominio.Commands
{
    public class AdicionarAudioCommand
    {
        public string Titulo { get; set; }
        public byte[] Bytes { get; set; }
        public int IdUsuario { get; set; }
    }
}
