using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nunos2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MensagensPage : ContentPage
    {
        private bool Atualizou = false;

        public MensagensPage()
        {
            InitializeComponent();
            
            List<string> lstOpcoes = new List<string>
            {
                "Todos",
                "Roubos",
                "Acidentes",
                "Bem-Estar"
            };

            cmbOpcoes.ItemsSource = lstOpcoes;

            var menuItems = new List<ItemMensagem>
            {
                new ItemMensagem {Id = 1, Imagem = "https://nunos.s3.amazonaws.com/caminhoneiro-5.png", Logo="https://nunos.s3.amazonaws.com/caminhao-quebrou.png", NumLikes=5, NumComentarios=2, Remetente = "José da Silva", Mensagem = "Pessoal, meu caminhão quebrou aqui na Rodovia Anchieta, km 15. Será que alguém consegue me ajudar?" },
                new ItemMensagem {Id = 2, Imagem = "https://nunos.s3.amazonaws.com/caminhoneiro-6.png", Logo="https://nunos.s3.amazonaws.com/bem-estar.png", NumLikes=2, NumComentarios=3, Remetente = "Rafael Tavares", Mensagem = "Parei no ponto do km 32 da Bandeirantes e fiz uma massagem ótima! Me sinto renovado. Recomendo!" },
                new ItemMensagem {Id = 3, Imagem = "https://nunos.s3.amazonaws.com/caminhoneiro-7.png", Logo="https://nunos.s3.amazonaws.com/buraco-na-rua.png", NumLikes=4, NumComentarios=0, Remetente = "Rogério Fernandes", Mensagem = "Cuidado com a Rodovia Raposo Tavares, pois surgiu um buraco enorme na via próx. ao km 15!" },
                new ItemMensagem {Id = 4, Imagem = "https://nunos.s3.amazonaws.com/caminhoneiro-9.png", Logo="https://nunos.s3.amazonaws.com/restaurantes.png", NumLikes=1, NumComentarios=1, Remetente = "Giovani Oliveira", Mensagem = "Recomendo comerem no restaurante Arapiás, próx. ao km 22 da Castelo Branco. Comida boa e barata!" }
            };

            ListViewMenu.ItemsSource = menuItems;

            //InsereGridPosto();
        }

        private void InsereGridComentarios()
        {
            var menuItems = new List<ItemMensagem>
            {
                new ItemMensagem {Id = 1, Imagem = "https://nunos.s3.amazonaws.com/caminhoneiro-8.png", Logo="https://nunos.s3.amazonaws.com/roubo.png", NumLikes=5, NumComentarios=2, Remetente = "José da Silva", Mensagem = "Pessoal, acabei de ser assaltado no km 41 da Tamoios. Fiquem espertos, pois os bandidos são ligeiros." },
            };

            ListViewMenu.ItemsSource = menuItems;

            Device.StartTimer(TimeSpan.FromSeconds(7), () =>
            {
                Device.BeginInvokeOnMainThread(() => AtualizaGridComentarios());
                return true;
            });
        }

        private void AtualizaGridComentarios()
        {
            if (!Atualizou)
            {
                var menuItems = new List<ItemMensagem>
                {
                    new ItemMensagem {Id = 1, Imagem = "https://nunos.s3.amazonaws.com/caminhoneiro-8.png", Logo="https://nunos.s3.amazonaws.com/roubo.png", NumLikes=5, NumComentarios=2, Remetente = "José da Silva", Mensagem = "Pessoal, acabei de ser assaltado no km 41 da Tamoios. Fiquem espertos, pois os bandidos são ligeiros." },
                    new ItemMensagem {Id = 1, Imagem = "https://nunos.s3.amazonaws.com/logo-ccr.png", Logo="https://nunos.s3.amazonaws.com/roubo.png", NumLikes=5, NumComentarios=2, Remetente = "CCR", Mensagem = "Olá José. Estamos monitorando o seu caminhão e já acionamos a polícia interceptar os bandidos. Nossa equipe irá te ligar." }
                };

                ListViewMenu.ItemsSource = menuItems;
                Atualizou = true;
            }
        }

        private void InsereGridPosto()
        {
            var menuItems = new List<ItemMensagem>
            {
                new ItemMensagem {Id = 1, Imagem = "https://mileniobus.com.br/img/marcel_ogando.jpg", NumLikes=5, NumComentarios=2, Remetente = "Marcel Ogando", Mensagem = "Pessoal, aproveitem que o posto aqui no km 35 da Bandeirantes está com um preço ótimo! Abasteçam aqui." }
            };

            ListViewMenu.ItemsSource = menuItems;

            Device.StartTimer(TimeSpan.FromSeconds(7), () =>
            {
                Device.BeginInvokeOnMainThread(() => AtualizaGridPosto());
                return true;
            });
        }

        private void AtualizaGridPosto()
        {
            if (!Atualizou)
            {
                var menuItems = new List<ItemMensagem>
            {
                new ItemMensagem {Id = 1, Imagem = "https://mileniobus.com.br/img/marcel_ogando.jpg", NumLikes=5, NumComentarios=2, Remetente = "Marcel Ogando", Mensagem = "Pessoal, aproveitem que o posto aqui no km 35 da Bandeirantes está com um preço ótimo! Abasteçam aqui." },
                new ItemMensagem { Id = 1, Imagem = "https://scontent.fcgh22-1.fna.fbcdn.net/v/t1.0-9/s960x960/76682984_2665793573441583_3850807754209886208_o.jpg?_nc_cat=100&_nc_sid=85a577&_nc_ohc=fXvxhvKy9osAX_GKjdS&_nc_ht=scontent.fcgh22-1.fna&_nc_tp=7&oh=df6b5db10ab6ea8d7bac180973b935d6&oe=5F0DD39E", NumLikes = 1, NumComentarios = 0, Remetente = "Vitor Bonetti", Mensagem = "Boooa! Esse posto fica bem no caminho da rota que estou percorrendo. Estou indo abastecer aí agora!" }
            };

                ListViewMenu.ItemsSource = menuItems;

                Atualizou = true;
            }
        }

        private void cmbOpcoes_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Picker cmb = (Picker)sender;

            if (cmb.SelectedItem == "Roubos")
            {
                InsereGridComentarios();
            }
        }
    }

}