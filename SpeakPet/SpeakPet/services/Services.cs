using Dominio.Interfaces;
using Service;

namespace SpeakPet.services
{
    public static class Services
    {
        private static IUsuarioService usuarioService;
        private static IAudioService audioSerivce;
        private static ISpeakPetService speakPetService;
       

        public static IUsuarioService GetUsuarioService()
        {
            if (usuarioService == null)
                usuarioService = new UsuarioService();
            return usuarioService;
        }

        public static IAudioService GetAudioService()
        {
            if (audioSerivce == null)
                audioSerivce = new AudioService();
            return audioSerivce;
        }

        public static ISpeakPetService GetSpeakPetService()
        {
            if (speakPetService == null)
                speakPetService = new SpeakPetService();
            return speakPetService;
        }
    }
}
