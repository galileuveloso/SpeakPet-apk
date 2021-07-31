using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Interfaces.Api;
using Dominio.Responses;
using Service.Api;

namespace Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioApiService usuarioApiService;

        public UsuarioService(string urlBase)
        {
            usuarioApiService = new UsuarioApiService(urlBase);
        }

        public EfetuarLoginResponse EfetuarLogin(string login, string senha)
        {
            return usuarioApiService.EfetuarLogin(new EfetuarLoginCommand(login, senha)).GetAwaiter().GetResult();
        }

        public InserirUsuarioResponse InserirUsuario(string login, string senha)
        {
            return usuarioApiService.InserirUsuario(new InserirUsuarioCommand(login, senha)).GetAwaiter().GetResult();
        }
    }
}
