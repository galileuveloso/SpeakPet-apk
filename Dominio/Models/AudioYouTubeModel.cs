namespace Dominio.Models
{
    public class AudioYouTubeModel
    {
        public AudioYouTubeModel(string titulo, string linkYouTube, int idUsuario)
        {
            Titulo = titulo;
            LinkYouTube = linkYouTube;
            IdUsuario = idUsuario;
        }

        public string Titulo { get; set; }
        public string LinkYouTube { get; set; }
        public int IdUsuario { get; set; }
    }
}
