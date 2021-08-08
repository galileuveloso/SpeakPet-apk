using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Interfaces.Api;
using Dominio.Models;
using Dominio.Responses;
using Service.Api;
using System;

namespace Service
{
    public class ReproducaoService : IReproducaoService
    {
        private readonly IReproducaoApiService reproducaoApiService;

        public ReproducaoService(string urlBase)
        {
            reproducaoApiService = new ReproducaoApiService(urlBase);
        }

        public InserirReproducaoResponse InserirReproducao(int idAudio, int idUsuario)
        {
            return reproducaoApiService.InserirReproducao(new InserirReproducaoCommand(new ReproducaoModel(idAudio, idUsuario, DateTime.Now, true))).GetAwaiter().GetResult();
        }

        public DesativarReproducaoResponse DesativarReproducao(int? idReproducao)
        {
            return reproducaoApiService.DesativarReproducao(new DesativarReproducaoCommand(idReproducao)).GetAwaiter().GetResult();
        }
    }
}