using Dominio.Commands;
using Dominio.Responses;
using SpeakPet.services;
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

        private async void Login_Clicked(object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(login.Text) || String.IsNullOrEmpty(password.Text))
                await DisplayAlert("Dados Incompletos", "Preencha todos os campos", "Ok");
            else
            {
                try
                {
                    EfetuarLoginCommand command = new EfetuarLoginCommand(login.Text, password.Text);
                    EfetuarLoginResponse response = Services.GetUsuarioService().EfetuarLogin(command).GetAwaiter().GetResult();

                    if (response.Sucesso == true && response.IdUsuario == null)
                    {
                        await DisplayAlert("Usuário Inválido", response.Mensagem, "Ok");
                    }
                    else if (response.Sucesso == false)
                    {
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    }
                    else
                    {
                        //chamo a tela inicial
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
                }
            }
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }
    }
}
