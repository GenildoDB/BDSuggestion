using BDSuggestion.Models;
using BDSuggestion.Services;
using BDSuggestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDSuggestion.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private SugestaoCollection Collection;

        public Home()
        {
            Collection = new SugestaoCollection(false);
            InitializeComponent();
            BindingContext = Collection;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            //CarregarSugestaoGlobal();
            Collection.CarregarSugestaoGlobal();
        }

        private void PickerFiltrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null && sender is Picker picker && picker.SelectedItem != null && picker.SelectedItem is Departamentos depart)
            {
                if (!depart.Nome.Equals("Ver todos"))
                {
                    var filter = new ObservableCollection<SugestaoViewModel>(Collection.ListaSugestao.Where(p => p.Departamento.Id == depart.Id));
                    ListViewSugestoes.ItemsSource = filter;
                }
                else
                {
                    ListViewSugestoes.ItemsSource = Collection.ListaSugestao;
                }
            }
        }

        private async void ListViewSugestoes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null && e.SelectedItem is SugestaoViewModel Sugs)
            {
                await Navigation.PushAsync(new PageCadSugestao(Sugs), true);
            }
            ListViewSugestoes.SelectedItem = null;
        }

        private async void AddSugestao_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCadSugestao(), true);
        }
    }
}