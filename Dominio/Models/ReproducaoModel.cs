using System;

namespace Dominio.Models
{
    public class ReproducaoModel
    {
        public ReproducaoModel(int idAudio, int idUsuario, DateTime dataReproducao, bool ativo)
        {
            IdAudio = idAudio;
            IdUsuario = idUsuario;
            DataReproducao = dataReproducao;
            Ativo = ativo;
        }

        public int Id { get; set; }
        public int IdAudio { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataReproducao { get; set; }
        public bool Ativo { get; set; }
    }
}