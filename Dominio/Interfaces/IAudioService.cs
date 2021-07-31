﻿using Dominio.Commands;
using Dominio.Responses;
using Refit;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IAudioService
    {
        byte[] LerBytesAudio(Stream stream);
        Task<AdicionarAudioResponse> AdicionarAudio(AdicionarAudioCommand command);
        Task<AdicionarAudioYouTubeResponse> AdicionarAudioYouTube(AdicionarAudioYouTubeCommand command);
        Task<ListarAudiosResponse> ListarAudios(int idUsuaio);
        Task<ExcluirAudioResponse> ExcluirAudio(ExcluirAudioCommand command);
        Task<EditarAudioResponse> EditarAudio(EditarAudioCommand command);
    }

    public interface IAudioServiceApi
    {
        [Post("/audio/reproduzir")]
        Task<HttpContent> ReproduzirAudio([Body] ReproduzirAudioCommand command);

        [Post("/audio/adicionar")]
        Task<HttpContent> AdicionarAudio([Body] AdicionarAudioCommand command);

        [Post("/audio/adicionaryt")]
        Task<HttpContent> AdicionarAudioYouTube([Body] AdicionarAudioYouTubeCommand command);

        [Delete("/audio/excluir")]
        Task<HttpContent> ExcluirAudio([Body] ExcluirAudioCommand command);

        [Patch("/audio/editar")]
        Task<HttpContent> EditarAudio([Body] EditarAudioCommand command);

        [Get("/audio/listar/{idUsuario}")]
        Task<HttpResponseMessage> ListarAudios([AliasAs("idUsuario")] int idUsuario);
    }
}
