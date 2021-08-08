namespace Dominio.Commands
{
    public class DesativarReproducaoCommand
    {
        public DesativarReproducaoCommand(int? idReproducao)
        {
            IdReproducao = idReproducao;
        }

        public int? IdReproducao { get; set; }
    }
}
