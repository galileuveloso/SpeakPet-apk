namespace Dominio.Commands
{
    public class EfetuarLoginCommand
    {
        public EfetuarLoginCommand(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
