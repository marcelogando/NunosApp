using Nunos2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using System.Threading;

namespace Nunos2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private List<Position> posicoes = new List<Position>();
        private int posAtual = 0;
        private int contAtualizacoes = 0;

        public MainPage()
        {
            InitializeComponent();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-23.604990, -46.679282), Distance.FromMeters(25000)));
        }

        public void PlotaRota()
        {
            var model = new MainViewModel();
            string latA = "-23.431580";
            string lonA = "-46.760472";
            string latB = "-23.816403";
            string lonB = "-46.583936";

            posicoes = model.TracarRota(latA, lonA, latB, lonB);
            posAtual = posicoes.Count() - 1;

            map.Polylines.Clear();
            map.Polygons.Clear();

            Polyline line = new Polyline();
            line.StrokeWidth = 10f;
            line.StrokeColor = Color.Blue;

            foreach (Position pos in posicoes)
            {
                line.Positions.Add(pos);
            }

            map.Polylines.Add(line);

            Pin pinA = new Pin()
            {
                Type = PinType.Place,
                Label = "Rod. dos Bandeirantes",
                Address = "Rod. dos Bandeirantes",
                Position = new Position(Convert.ToDouble(latA), Convert.ToDouble(lonA)),
                Tag = "id_bandeirantes",
                IsVisible = true
            };

            map.Pins.Add(pinA);

            Pin pinB = new Pin()
            {
                Type = PinType.Place,
                Label = "Rod. dos Imigrantes",
                Address = "Rod. dos Imigrantes",
                Position = new Position(Convert.ToDouble(latB), Convert.ToDouble(lonB)),
                Tag = "id_imigrantes",
                IsVisible = true
            };

            //map.Pins.Add(pinB);


            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-23.604990, -46.679282), Distance.FromMeters(25000)));

            comecarViagem.IsVisible = true;
        }

        public void ComecaViagem(object sender, EventArgs e)
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-23.816403, -46.583936), Distance.FromMeters(100)));

            comecarViagem.IsVisible = false;
            stopRouteButton.IsVisible = true;
            pinPonteiro.IsVisible = true;

            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                Device.BeginInvokeOnMainThread(() => refreshViagem());
                return true;
            });
        }

        public async void MandouMensagem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MensagensPage());
        }

        private void refreshViagem()
        {
            if (contAtualizacoes <= 10)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(posicoes[posAtual], Distance.FromMeters(100)));
                posAtual--;

                contAtualizacoes++;

                if (contAtualizacoes == 11)
                {
                    pinPosto.IsVisible = true;
                }
            }
            else
            {
                pinPosto.IsVisible = true;
                pinPonteiro.HorizontalOptions = LayoutOptions.Start;
                pinPonteiro.Margin = new Thickness(20, 0);
                
            }
        }


        public async void OnEnterAddressTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPlacePage() { BindingContext = this.BindingContext }, false);

        }

        public void HandleStopClicked(object sender, EventArgs e)
        {
            searchLayout.IsVisible = true;
            stopRouteButton.IsVisible = false;
            map.Polylines.Clear();
            map.Pins.Clear();
        }

        public void SelecionaPrecos(object sender, EventArgs e)
        {
            if (BotaoPrecos.BackgroundColor == Color.White)
            {
                BotaoPrecos.BackgroundColor = Color.FromHex("#5b9ed2");
                BotaoPrecos.HorizontalOptions = LayoutOptions.Center;
                BotaoPrecos.SetValue(HeightRequestProperty, 80);
                BotaoPrecos.SetValue(WidthRequestProperty, 80);
                BotaoPrecos.SetValue(Grid.RowProperty, 2);
                BotaoPrecos.SetValue(Grid.ColumnProperty, 0);
            }
            else
            {
                BotaoPrecos.BackgroundColor = Color.White;
                BotaoPrecos.HorizontalOptions = LayoutOptions.Center;
                BotaoPrecos.SetValue(HeightRequestProperty, 80);
                BotaoPrecos.SetValue(WidthRequestProperty, 80);
                BotaoPrecos.SetValue(Grid.RowProperty, 2);
                BotaoPrecos.SetValue(Grid.ColumnProperty, 0);
            }
        }

        //Center map in actual location 
        protected override void OnAppearing()
        {
            base.OnAppearing();
            map.GetActualLocationCommand.Execute(null);
        }

        void OnCalculate(System.Object sender, System.EventArgs e)
        {
            searchLayout.IsVisible = false;
            stopRouteButton.IsVisible = true;
        }

        async void OnLocationError(System.Object sender, System.EventArgs e)
        {
            await DisplayAlert("Error", "Unable to get actual location", "Ok");
        }

        private void EnviarMensagem(object sender, EventArgs e)
        {
            //chooseLocationButton.IsVisible = false;
            //opcoesLayout.IsVisible = true;

            Navigation.PushAsync(new MensagensPage());
        }
    }
}