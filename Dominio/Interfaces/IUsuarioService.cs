using Dominio.Responses;

namespace Dominio.Interfaces
{
    public interface IUsuarioService
    {
        EfetuarLoginResponse EfetuarLogin(string login, string senha);
        InserirUsuarioResponse InserirUsuario(string login, string senha);
    }
}
