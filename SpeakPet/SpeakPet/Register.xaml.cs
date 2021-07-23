using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Responses;
using SpeakPet.services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeakPet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private readonly IUsuarioService usuarioService;
        public Register()
        {
            InitializeComponent();
            usuarioService = Services.GetUsuarioService();
        }

        protected async void Registrar_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(login.Text) || String.IsNullOrEmpty(password.Text) || String.IsNullOrEmpty(passwordconfirm.Text))
                await DisplayAlert("Dados Incompletos", "Preencha todos os campos", "Ok");
            else if (password.Text != passwordconfirm.Text)
                await DisplayAlert("Dados Incoerentes", "As Senhas inseridas não são iguais.", "Ok");
            else
            {
                try
                {
                    InserirUsuarioCommand command = new InserirUsuarioCommand(login.Text, password.Text);
                    InserirUsuarioResponse response = usuarioService.InserirUsuario(command).GetAwaiter().GetResult();

                    if (response.Sucesso == false)
                    {
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    }
                    else
                    {
                        //chamo o home com as informacoes
                    }
                }
                catch
                {
                    await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");

                }
            }
        }
    }
}