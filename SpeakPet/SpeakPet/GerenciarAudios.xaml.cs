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

        public GerenciarAudios()
        {
            InitializeComponent();
            audioService = Services.GetAudioService();
            PreencherAudios();
            BindingContext = this;
        }

        private void PreencherAudios()
        {
            //TODO - estudar isso pois bater sempre nesse cara pode ser ruim caso demore a request
            Audios = audioService.ListarAudios(Services.IdUsuarioLogado).GetAwaiter().GetResult().Audios.ToList();
            listaAudios.ItemsSource = Audios;
        }

        private ExcluirAudioResponse ExcluirAudio(int idAudio)
        {
            ExcluirAudioCommand command = new ExcluirAudioCommand(idAudio);
            return audioService.ExcluirAudio(command).GetAwaiter().GetResult();
        }

        private async void ValidarSucessoExclusao(ExcluirAudioResponse response)
        {
            if (response.Sucesso == false)
                await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
            else
                await DisplayAlert("Sucesso!", "Audio excluído com sucesso.", "Ok");
        }

        private async void AdicionarAudio_Clicked(object sender, EventArgs e)
        {
            FileResult audioUpload = null;
            try
            {
                var customFile = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] {"audio/x-mp3", "audio/mp3", "audio/mpeg3", "audio/mpeg" } },
                    { DevicePlatform.iOS, new[] { "audio/x-mp3", "audio/mp3", "audio/mpeg3", "audio/mpeg" } },
                    { DevicePlatform.Unknown, new[] { "audio/x-mp3", "audio/mp3", "audio/mpeg3", "audio/mpeg" } }
                });
                audioUpload = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = customFile,
                    PickerTitle = "Selecionar Audio"
                });

                if (audioUpload == null)
                    await DisplayAlert("Nenhum audio selecionado.", "Selecione ao menos um audio.", "Tentar Novamente");
                else
                {
                    string titulo = await DisplayPromptAsync("Selecionar Audio", "Titulo:", "Ok", "Cancelar", "Insira um titulo...", 255, Keyboard.Text, audioUpload.FileName);

                    if (titulo != null && String.IsNullOrEmpty(titulo))
                        await DisplayAlert("Titulo Invalido.", "O Titulo do áudio não pode ser vazio.", "Tentar Novamente");
                    else if (titulo == null)
                        return;

                    Stream stream = await audioUpload.OpenReadAsync();

                    AdicionarAudioCommand command = new AdicionarAudioCommand(titulo, audioService.LerBytesAudio(stream), Services.IdUsuarioLogado);
                    AdicionarAudioResponse response = audioService.AdicionarAudio(command).GetAwaiter().GetResult();

                    if (response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                    {
                        await DisplayAlert("Sucesso!", "Audio adicionado com sucesso.", "Ok");
                        PreencherAudios();
                    }
                }
            }
            catch
            {
                if(audioUpload == null)
                    await DisplayAlert("Erro", "Erro ao tentar abrir os audios...", "Tentar Novamente");
                else
                    await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
            }
        }

        private void listaAudios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void listaAudios_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void ExcluirAudioButton_Clicked(object sender, EventArgs e)
        {
            bool confirmacao = await DisplayAlert("Excluir Audio", "Deseja mesmo excluir o audio selecionado?", "Confirmar", "Cancelar");

            if (confirmacao)
            {
                try
                {
                    int idAudio = int.Parse((sender as Button).CommandParameter.ToString());
                    ExcluirAudioResponse response = ExcluirAudio(idAudio);
                    ValidarSucessoExclusao(response);
                    PreencherAudios();
                }
                catch
                {
                    await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");

                }
            }
        }

        private async void EditarAudioButton_Clicked(object sender, EventArgs e)
        {
            AudioModel audio = Audios.Where(x => x.Id == int.Parse((sender as Button).CommandParameter.ToString())).FirstOrDefault();
            string novoTitulo = await DisplayPromptAsync("Editar Audio", "Titulo:", "Ok", "Cancelar", "Insira um titulo...", 255, Keyboard.Text, audio.Titulo);

            if (novoTitulo != null && String.IsNullOrEmpty(novoTitulo))
                await DisplayAlert("Titulo Invalido.", "O Titulo do áudio não pode ser vazio.", "Tentar Novamente");
            else if (novoTitulo == null)
                return;
            else if (novoTitulo == audio.Titulo)
                await DisplayAlert("Sem mudanças", "Não houve nenhuma alteração.", "Ok");
            else
            {
                try
                {
                    EditarAudioCommand command = new EditarAudioCommand(audio.Id.Value, novoTitulo);
                    EditarAudioResponse response = audioService.EditarAudio(command).GetAwaiter().GetResult();

                    if (response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                        await DisplayAlert("Sucesso!", "Audio editado com sucesso.", "Ok");

                    PreencherAudios();
                }
                catch
                {
                    await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
                }
            }
        }
    }
}