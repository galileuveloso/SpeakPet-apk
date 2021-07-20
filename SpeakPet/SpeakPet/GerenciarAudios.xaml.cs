using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Responses;
using SpeakPet.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeakPet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GerenciarAudios : ContentPage
    {
        private readonly IAudioService audioService;

        private IList<AudioModel> Audios { get; set; }

        private FileResult AudioSelecionado { get; set; }
        
        public GerenciarAudios()
        {
            InitializeComponent();
            audioService = Services.GetAudioService();
            PreencherAudios();
        }

        private async void AdicionarAudio_Clicked(object sender, EventArgs e)
        {
            try
            {
                AudioSelecionado = await FilePicker.PickAsync(new PickOptions
                {
                    //TODO - Ver como selecionar o formato de audio apenas
                    FileTypes = FilePickerFileType.Videos,
                    PickerTitle = "Selecionar Audio"
                });
                fileName.Text = AudioSelecionado.FileName;
            }
            catch
            {
                await DisplayAlert("Erro", "Erro ao tentar abrir os audios...", "Tentar Novamente");
            }
        }

        private async void SalvarAudio_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (AudioSelecionado == null)
                    await DisplayAlert("Nenhum audio selecionado.", "Selecione ao menos um audio.", "Tentar Novamente");
                else if (String.IsNullOrEmpty(AudioSelecionado.FileName))
                    await DisplayAlert("Nome Invalido.", "O Nome do áudio não pode ser vazio.", "Tentar Novamente");
                else
                {
                    Stream stream = await AudioSelecionado.OpenReadAsync();

                    AdicionarAudioCommand command = new AdicionarAudioCommand(fileName.Text, audioService.LerBytesAudio(stream), Services.IdUsuarioLogado);
                    AdicionarAudioResponse response = audioService.AdicionarAudio(command).GetAwaiter().GetResult();

                    if (response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                    {
                        await DisplayAlert("Sucesso!", "Audio adicionado com sucesso.", "Ok");
                        AudioSelecionado = null;
                        fileName.Text = "";
                    }
                }
            }
            catch
            {
                await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
            }
        }   

        private void PreencherAudios()
        {
            Audios = audioService.ListarAudios(Services.IdUsuarioLogado).GetAwaiter().GetResult().Audios.ToList();
            listaAudios.ItemsSource = Audios;
            BindingContext = this;
        }

        private void listaAudios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        private void listaAudios_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}