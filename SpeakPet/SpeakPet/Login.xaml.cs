using Dominio.Commands;
using System;
using Xamarin.Forms;

namespace SpeakPet
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, System.EventArgs e)
        {
            EfetuarLoginCommand command = new EfetuarLoginCommand(login.Text, password.Text);

            try
            {
                //EfetuarLoginResponse response = Services.GetSpeakPetService().EfetuarLogin(command).GetAwaiter().GetResult();

                //ListarAudiosResponse audios = Services.GetAudioService().ListarAudios(1).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {

            }

        }
    }
}
