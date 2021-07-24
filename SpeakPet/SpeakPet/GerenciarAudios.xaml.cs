using Dominio.Commands;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Responses;
using SpeakPet.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        private FileResult AudioUpload { get; set; }

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
            try
            {
                AudioUpload = await FilePicker.PickAsync(new PickOptions
                {
                    //TODO - Ver como selecionar o formato de audio apenas
                    FileTypes = FilePickerFileType.Videos,
                    PickerTitle = "Selecionar Audio"
                });
                fileName.Text = AudioUpload.FileName;
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
                if (AudioUpload == null)
                    await DisplayAlert("Nenhum audio selecionado.", "Selecione ao menos um audio.", "Tentar Novamente");
                else if (String.IsNullOrEmpty(AudioUpload.FileName))
                    await DisplayAlert("Nome Invalido.", "O Nome do áudio não pode ser vazio.", "Tentar Novamente");
                else
                {
                    Stream stream = await AudioUpload.OpenReadAsync();
                    
                    AdicionarAudioCommand command = new AdicionarAudioCommand(fileName.Text, audioService.LerBytesAudio(stream), Services.IdUsuarioLogado);
                    AdicionarAudioResponse response = audioService.AdicionarAudio(command).GetAwaiter().GetResult();

                    if (response.Sucesso == false)
                        await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                    else
                    {
                        await DisplayAlert("Sucesso!", "Audio adicionado com sucesso.", "Ok");
                        AudioUpload = null;
                        fileName.Text = "";
                        PreencherAudios();
                    }
                }
            }
            catch
            {
                await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
            }
        }

        private void listaAudios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            fileName.Text = (listaAudios.SelectedItem as AudioModel).Titulo;
        }

        private void listaAudios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            AudioUpload = null;
        }

        private async void ExcluirAudio_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (listaAudios.SelectedItem == null)
                    await DisplayAlert("Erro", "Nenhum audio foi selecionado.", "Tentar Novamente");
                else
                {
                    bool confirmacao = await DisplayAlert("Excluir Audio", "Deseja mesmo excluir o audio selecionado?", "Confirmar", "Cancelar");

                    if (confirmacao)
                    {
                        ExcluirAudioResponse response = ExcluirAudio((listaAudios.SelectedItem as AudioModel).Id.Value);
                        ValidarSucessoExclusao(response);
                        PreencherAudios();
                    }
                }
            }
            catch
            {
                await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
            }
        }

        private async void SalvarEdicaoAudio_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(fileName.Text))
                    await DisplayAlert("´Titulo Invalido.", "O Titulo do áudio não pode ser vazio.", "Tentar Novamente");
                else
                {
                    AudioModel audio = listaAudios.SelectedItem as AudioModel;

                    bool confirmacao = await DisplayAlert("Editar Audio", "Deseja alterar de '" + audio.Titulo + "' para '" + fileName.Text + "'?", "Confirmar", "Cancelar");

                    if (confirmacao)
                    {
                        EditarAudioCommand command = new EditarAudioCommand(audio.Id.Value, fileName.Text);
                        EditarAudioResponse response = audioService.EditarAudio(command).GetAwaiter().GetResult();

                        if (response.Sucesso == false)
                            await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                        else
                            await DisplayAlert("Sucesso!", "Audio editado com sucesso.", "Ok");

                        PreencherAudios();

                        fileName.Text = "";
                    }
                }
            }
            catch
            {
                await DisplayAlert("Erro", "Algo está atrapalhando a conexão com o servidor...", "Tentar Novamente");
            }
        }

        private async void ExcluirAudioButton_Clicked(object sender, EventArgs e)
        {
            bool confirmacao = await DisplayAlert("Excluir Audio", "Deseja mesmo excluir o audio selecionado?", "Confirmar", "Cancelar");

            if (confirmacao)
            {
                int idAudio = int.Parse((sender as Button).CommandParameter.ToString());
                ExcluirAudioResponse response = ExcluirAudio(idAudio);
                ValidarSucessoExclusao(response);
                PreencherAudios();
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
                EditarAudioCommand command = new EditarAudioCommand(audio.Id.Value, novoTitulo);
                EditarAudioResponse response = audioService.EditarAudio(command).GetAwaiter().GetResult();

                if (response.Sucesso == false)
                    await DisplayAlert("Erro", response.Mensagem, "Tentar Novamente");
                else
                    await DisplayAlert("Sucesso!", "Audio editado com sucesso.", "Ok");

                PreencherAudios();
            }     
        }
    }
}