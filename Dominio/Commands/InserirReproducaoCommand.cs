using Dominio.Models;

namespace Dominio.Commands
{
    public class InserirReproducaoCommand
    {
        public InserirReproducaoCommand(ReproducaoModel reproducao)
        {
            Reproducao = reproducao;
        }

        public ReproducaoModel Reproducao { get; set; }
    }
}
