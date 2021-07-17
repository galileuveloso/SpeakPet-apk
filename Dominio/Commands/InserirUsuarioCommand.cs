namespace Dominio.Commands
{
    public class InserirUsuarioCommand
    {
        public InserirUsuarioCommand(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
