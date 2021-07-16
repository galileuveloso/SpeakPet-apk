using System;

namespace Dominio.Models
{
    [Serializable]
    public class UsuarioModel
    {
        public UsuarioModel(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public int? Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
