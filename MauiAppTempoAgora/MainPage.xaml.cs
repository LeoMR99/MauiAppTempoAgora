using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
             

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Erro", "Sem conexão com a internet. Verifique sua conexão.", "OK");
                }

                if(!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                            $"Longitude: {t.lon} \n" +
                            $"Nascer do Sol {t.sunrise} \n" +
                            $"Por do Sol: {t.sunset} \n" +
                            $"Temp Máx: {t.temp_max} \n" +
                            $"Temp Min: {t.temp_min} \n" +
                            $"Descrição Textual do Clima: {t.description} \n" +
                            $"Velocidade do Vento: {t.speed} \n" +
                            $"Visibilidade: {t.visibility} \n";


                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        await DisplayAlert("Erro", "Cidade não encontrada", "OK");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DisplayAlert("Erro", "Cidade não encontrada", "OK");
                }
                else
                {
                    await DisplayAlert("Erro", $"Erro na requisição: {ex.Message}", "OK");
                }
            }
            catch (Exception ex) 
            {
                await DisplayAlert("Ops", $"Ocorreu um erro inesperado: {ex.Message}", "OK");
            }
        }
                
            }
        }    
