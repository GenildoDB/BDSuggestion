using BDSuggestion.Services;
using BDSuggestion.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDSuggestion.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListDepart : ContentPage
    {
        public ObservableCollection<DepartamentoViewModel> ListDepart;

        public PageListDepart()
        {
            InitializeComponent();

            ListDepart = new ObservableCollection<DepartamentoViewModel>();

            MyListView.ItemsSource = ListDepart;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ListDepart.Clear();
            DepartamentoDB dB = new DepartamentoDB();
            var list = await dB.ListarDepartamentos();
            foreach (var dep in list)
                ListDepart.Add(new DepartamentoViewModel()
                {
                    Departamento = dep
                });
        }


        /// <summary>
        /// Exclui o departamento
        /// </summary>
        private async void SwipeItemExcluir_Clicked(object sender, EventArgs e)
        {
            if (sender != null && sender is SwipeItem item && item.BindingContext is DepartamentoViewModel Depart)
            {
                DepartamentoDB dB = new DepartamentoDB();
                int result = await dB.Deletar(Depart.Departamento.Id);
                if (result > 0)
                {
                    ListDepart.Remove(Depart);
                    await DisplayAlert("", "Departamento excluído com sucesso!", "OK");
                }
                else
                {
                    await DisplayAlert("", "Ocorreu algum erro ao excluir o departamento", "OK");
                }
            }
        }

        /// <summary>
        /// Edita o departamento
        /// </summary>
        private async void SwipeItemEditar_Clicked(object sender, EventArgs e)
        {
            if (sender != null && sender is SwipeItem item && item.BindingContext is DepartamentoViewModel Depart)
            {
                await Navigation.PushAsync(new PageCadDepart(Depart), true);
            }
        }
    }
}
