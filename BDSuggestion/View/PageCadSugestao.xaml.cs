using BDSuggestion.Models;
using BDSuggestion.Services;
using BDSuggestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDSuggestion.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadSugestao : ContentPage
    {
        public PageCadSugestao()
        {
            BindingContext = new SugestaoAddViewModel()
            {
                Sugestao = new Sugestoes(){ Colaborador = App.Colaborador },
                Departamento = new Departamentos() { }
            };
            InitializeComponent();

           
        }
        public PageCadSugestao(SugestaoViewModel sugs)
        {
            BindingContext = new SugestaoAddViewModel()
            {
                Sugestao = sugs.Sugestao,
                Departamento = sugs.Departamento
            };

            InitializeComponent();

            if (!sugs.Sugestao.Colaborador.Equals(App.Colaborador))
                Desabilitado();
        }

        private void Desabilitado()
        {
            EntryColab.IsEnabled = false;
            PickerDepart.IsEnabled = false;
            EditJust.IsEnabled = false;
            EditSugs.IsEnabled = false;
            BtnAdd.IsVisible = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                DepartamentoDB db = new DepartamentoDB();
                var lista = await db.ListarDepartamentos();
                PickerDepart.ItemsSource = lista;

                if (BindingContext != null && BindingContext is SugestaoViewModel Sugs && !string.IsNullOrWhiteSpace(Sugs.Sugestao.Departamento))
                    PickerDepart.SelectedItem = lista.FirstOrDefault(p => p.Nome.Equals(Sugs.Sugestao.Departamento));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", string.Format("{0}\n{1}", ex.Message, ex.StackTrace), "OK");
            }
        }
       
        private void PickerDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BindingContext != null && BindingContext is SugestaoViewModel Sugs)
            {
               if(PickerDepart.SelectedItem != null && PickerDepart.SelectedItem is Departamentos depart)
                {
                    Sugs.Departamento.Id = depart.Id;
                    Sugs.Departamento.Nome = depart.Nome;
                    Sugs.Sugestao.Departamento = depart.Nome;
                }
            }
        }
    }
}