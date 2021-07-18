using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Responses;
using SpeakPet.services;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeakPet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GerenciarAudios : ContentPage
    {
        private readonly IAudioService audioService;
        public GerenciarAudios()
        {
            InitializeComponent();
            audioService = Services.GetAudioService();
        }

        private async void AdicionarAudio_Clicked(object sender, EventArgs e)
        {
            try
            {
                var audio = await FilePicker.PickAsync(new PickOptions
                {
                    //TODO - Ver como selecionar o formato de audio apenas
                    FileTypes = FilePickerFileType.Videos,
                    PickerTitle = "Selecionar Audio"
                });

                if (audio != null)
                {
                    Stream stream = await audio.OpenReadAsync();

                    AdicionarAudioCommand command = new AdicionarAudioCommand(audio.FileName, audioService.LerBytesAudio(stream), Services.IdUsuarioLogado);
                    AdicionarAudioResponse response = audioService.AdicionarAudio(command).GetAwaiter().GetResult();

                    if(response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                        await DisplayAlert("Sucesso!", "Audio adicionado com sucesso.", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
            }
        }
    }
}