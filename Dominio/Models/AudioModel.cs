namespace Dominio.Models
{
    public class AudioModel
    {
        public int? Id { get; set; }
        public string Titulo { get; set; }
        public byte[] Bytes { get; set; }
        public int IdUsuario { get; set; }
    }
}
