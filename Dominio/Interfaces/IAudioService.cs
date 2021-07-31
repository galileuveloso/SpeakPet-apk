using Dominio.Models;
using Dominio.Responses;
using System.Collections.Generic;
using System.IO;

namespace Dominio.Interfaces
{
    public interface IAudioService
    {
        byte[] LerBytesAudio(Stream stream);
        AdicionarAudioResponse AdicionarAudio(IList<AudioModel> audios);
        AdicionarAudioYouTubeResponse AdicionarAudioYouTube(string titulo, string linkYouTube, int idUsuario);
        ListarAudiosResponse ListarAudios(int idUsuaio);
        ExcluirAudioResponse ExcluirAudio(int idAudio);
        EditarAudioResponse EditarAudio(int idAudio, string novoTitulo);
    }


}
