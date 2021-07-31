using Dominio.Interfaces;
using Dominio.Responses;
using SpeakPet.services;
using System;
using Xamarin.Forms;

namespace SpeakPet
{
    public partial class MainPage : ContentPage
    {
        private readonly IUsuarioService usuarioService;
        public MainPage()
        {
            InitializeComponent();
            usuarioService = Services.GetUsuarioService();
        }

        private async void Login_Clicked(object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(login.Text) || String.IsNullOrEmpty(password.Text))
                await DisplayAlert("Dados Incompletos", "Preencha todos os campos", "Ok");
            else
            {
                try
                {
                    EfetuarLoginResponse response = usuarioService.EfetuarLogin(login.Text, password.Text);

                    if (response.Sucesso == true && response.IdUsuario == null)
                        await DisplayAlert("Usuário Inválido", response.Mensagem, "Ok");
                    else if (response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                    {
                        Services.IdUsuarioLogado = response.IdUsuario.Value;
                        //chamo a tela inicial
                        await Navigation.PushAsync(new GerenciarAudios());
                        Navigation.RemovePage(this);
                    }
                }
                catch
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
