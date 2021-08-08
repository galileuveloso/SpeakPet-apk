using Dominio.Interfaces;
using Service;

namespace SpeakPet.services
{
    public static class Services
    {
        private static IUsuarioService usuarioService;
        private static IAudioService audioSerivce;
        private static IReproducaoService reproducaoSerivce;

        public static string urlBase = "http://192.168.0.13:5000";

        public static int IdUsuarioLogado { get; set; }

        public static IUsuarioService GetUsuarioService()
        {
            if (usuarioService == null)
                usuarioService = new UsuarioService(urlBase);
            return usuarioService;
        }

        public static IAudioService GetAudioService()
        {
            if (audioSerivce == null)
                audioSerivce = new AudioService(urlBase);
            return audioSerivce;
        }

        public static IReproducaoService GetReproducaoService()
        {
            if (reproducaoSerivce == null)
                reproducaoSerivce = new ReproducaoService(urlBase);
            return reproducaoSerivce;
        }
    }
}
