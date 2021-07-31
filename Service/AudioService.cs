using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Interfaces.Api;
using Dominio.Models;
using Dominio.Responses;
using Service.Api;
using System.Collections.Generic;
using System.IO;

namespace Service
{
    public class AudioService : IAudioService
    {
        private readonly IAudioApiService audioApiService;

        public AudioService(string urlBase)
        {
            audioApiService = new AudioApiService(urlBase);
        }

        public byte[] LerBytesAudio(Stream stream)
        {
            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mStream = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mStream.Write(byteArray, 0, bit);
                }
                return mStream.ToArray();
            }
        }

        public AdicionarAudioResponse AdicionarAudio(IList<AudioModel> audios)
        {
            return audioApiService.AdicionarAudio(new AdicionarAudioCommand(audios)).GetAwaiter().GetResult();
        }

        public AdicionarAudioYouTubeResponse AdicionarAudioYouTube(string titulo, string linkYouTube, int idUsuario)
        {
            return audioApiService.AdicionarAudioYouTube(new AdicionarAudioYouTubeCommand(new AudioYouTubeModel(titulo, linkYouTube, idUsuario))).GetAwaiter().GetResult();
        }

        public ListarAudiosResponse ListarAudios(int idUsuaio)
        {
            return audioApiService.ListarAudios(idUsuaio).GetAwaiter().GetResult();
        }

        public ExcluirAudioResponse ExcluirAudio(int idAudio)
        {
            return audioApiService.ExcluirAudio(new ExcluirAudioCommand(idAudio)).GetAwaiter().GetResult();
        }

        public EditarAudioResponse EditarAudio(int idAudio, string novoTitulo)
        {
            return audioApiService.EditarAudio(new EditarAudioCommand(idAudio, novoTitulo)).GetAwaiter().GetResult();
        }
    }
}
