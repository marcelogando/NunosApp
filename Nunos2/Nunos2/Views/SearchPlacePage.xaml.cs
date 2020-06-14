using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nunos2.Views
{
    public partial class SearchPlacePage : ContentPage
    {
        public static readonly BindableProperty FocusOriginCommandProperty =
   BindableProperty.Create(nameof(FocusOriginCommand), typeof(ICommand), typeof(SearchPlacePage), null, BindingMode.TwoWay);

        public ICommand FocusOriginCommand
        {
            get { return (ICommand)GetValue(FocusOriginCommandProperty); }
            set { SetValue(FocusOriginCommandProperty, value); }
        }


        public SearchPlacePage()
        {
            InitializeComponent();
        }

        public void BindingMudou(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        public void CalcularRota(object sender, EventArgs e)
        {
            MainPage page = new MainPage();
            page.PlotaRota();
            
            Navigation.PushAsync(page);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                FocusOriginCommand = new Command(OnOriginFocus);
            }
        }

        void OnOriginFocus()
        {
            destinationEntry.Focus();
        }
    }
}