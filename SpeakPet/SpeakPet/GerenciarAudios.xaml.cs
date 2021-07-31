using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Models.Visualizacao;
using Dominio.Responses;
using SpeakPet.services;
using System;
using System.Collections.Generic;
using System.Linq;
using VideoLibrary;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeakPet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GerenciarAudios : ContentPage
    {
        private readonly IAudioService audioService;

        private IList<ItemListaAudio> Audios { get; set; }

        public GerenciarAudios()
        {
            InitializeComponent();
            audioService = Services.GetAudioService();
            PreencherAudios();
            BindingContext = this;
        }

        private void PreencherAudios()
        {
            Audios = audioService.ListarAudios(Services.IdUsuarioLogado).Audios;
            listaAudios.ItemsSource = Audios;
        }

        private void listaAudios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void listaAudios_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void AdicionarAudio_Clicked(object sender, EventArgs e)
        {
            IEnumerable<FileResult> audiosUpload = new List<FileResult>();
            try
            {
                var customFile = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] {"audio/x-mp3", "audio/mp3", "audio/mpeg3", "audio/mpeg" } },
                    { DevicePlatform.iOS, new[] { "audio/x-mp3", "audio/mp3", "audio/mpeg3", "audio/mpeg" } },
                    { DevicePlatform.Unknown, new[] { "audio/x-mp3", "audio/mp3", "audio/mpeg3", "audio/mpeg" } }
                });
                audiosUpload = await FilePicker.PickMultipleAsync(new PickOptions
                {
                    FileTypes = customFile,
                    PickerTitle = "Selecionar Audio"
                });

                if (audiosUpload == null || audiosUpload.Count() == 0 || !audiosUpload.Any())
                    await DisplayAlert("Nenhum audio selecionado.", "Selecione ao menos um audio.", "Tentar Novamente");
                else
                {
                    IList<AudioModel> audios = new List<AudioModel>();
                    foreach (FileResult fileResult in audiosUpload)
                        audios.Add(new AudioModel(fileResult.FileName, audioService.LerBytesAudio(await fileResult.OpenReadAsync()), Services.IdUsuarioLogado));

                    AdicionarAudioResponse response = audioService.AdicionarAudio(audios);

                    if (response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                    {
                        await DisplayAlert("Sucesso!", "Audio(s) adicionado(s) com sucesso.", "Ok");
                        PreencherAudios();
                    }
                }
            }
            catch
            {
                if (audiosUpload == null)
                    await DisplayAlert("Erro", "Erro ao tentar abrir os audios...", "Tentar Novamente");
                else
                    await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
            }
        }

        private async void ExcluirAudioButton_Clicked(object sender, EventArgs e)
        {
            bool confirmacao = await DisplayAlert("Excluir Audio", "Deseja mesmo excluir o audio selecionado?", "Confirmar", "Cancelar");

            if (confirmacao)
            {
                try
                {
                    int idAudio = int.Parse((sender as Button).CommandParameter.ToString());
                    ExcluirAudioResponse response = audioService.ExcluirAudio(idAudio);

                    if (response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                        await DisplayAlert("Sucesso!", "Audio excluído com sucesso.", "Ok");

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
            AudioModel audio = new AudioModel(Audios.Where(x => x.Id == int.Parse((sender as Button).CommandParameter.ToString())).FirstOrDefault());
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
                    EditarAudioResponse response = audioService.EditarAudio(audio.Id.Value, novoTitulo);

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

        private async void AdicionarAudioYouTube_Clicked(object sender, EventArgs e)
        {
            string url = await DisplayPromptAsync("YouTube", "Insira o Link do video que deseja importar.");

            if (String.IsNullOrEmpty(url))
                return;
            else
            {
                YouTube yt = YouTube.Default;
                Video video;
                try
                {
                    video = yt.GetVideo(url);
                }
                catch
                {
                    await DisplayAlert("Erro", "Nenhum video encontrado com este link...", "Tentar Novamente");
                    return;
                }

                try
                {
                    AdicionarAudioYouTubeResponse response = audioService.AdicionarAudioYouTube(video.FullName, url, Services.IdUsuarioLogado);

                    if (response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                    {
                        await DisplayAlert("Sucesso!", "Audio adicionado com sucesso.", "Ok");
                        PreencherAudios();
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