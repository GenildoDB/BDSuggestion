using BDSuggestion.Models;
using BDSuggestion.Services;
using BDSuggestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDSuggestion.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListSugestao : ContentPage
    {
        private SugestaoCollection Collection;
        public PageListSugestao()
        {
            Collection = new SugestaoCollection(true, false);
            InitializeComponent();
            BindingContext = Collection;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           Collection.CarregarSugestaoGlobal();
        }

        /// <summary>
        /// Filtra as sugestões por departamento
        /// </summary>
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

        /// <summary>
        /// Exclui a sugestão do colaborador logado apenas
        /// </summary>
        private async void SwipeItemExcluir_Clicked(object sender, EventArgs e)
        {
            if (sender != null && sender is SwipeItem item && item.BindingContext is SugestaoViewModel Sugs)
            {
                SugestaoDB dB = new SugestaoDB();
                int result = await dB.Delete(Sugs.Sugestao.Id);
                if (result > 0)
                {
                    Collection.ListaSugestao.Remove(Sugs);
                    await DisplayAlert("", "Sugestão excluída com sucesso!", "OK");
                }
                else
                {
                    await DisplayAlert("", "Ocorreu algum erro ao excluir a sugestão", "OK");
                }
            }
        }

        /// <summary>
        /// Edita a sugestão do colaborador logado apenas
        /// </summary>
        private async void SwipeItemEditar_Clicked(object sender, EventArgs e)
        {
            if (sender != null && sender is SwipeItem item && item.BindingContext is SugestaoViewModel Sugs)
            {
                await Navigation.PushAsync(new PageCadSugestao(Sugs), true);
            }
        }
    }
}
