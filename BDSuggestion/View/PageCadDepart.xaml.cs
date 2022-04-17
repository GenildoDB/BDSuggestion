using BDSuggestion.ControlPersonalizado;
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
    public partial class PageCadDepart : ContentPage
    {
        public ObservableCollection<DepartamentoViewModel> ListDepart = new ObservableCollection<DepartamentoViewModel>();

        public PageCadDepart()
        {
            InitializeComponent();
            BindingContext = new DepartamentoAddViewModel() { Departamento = new Departamentos()};
           
            FlexDepart.BindingContext = ListDepart;
            Title = null;
        }

        public PageCadDepart(DepartamentoViewModel depart)
        {
            InitializeComponent();
            BindingContext = new DepartamentoAddViewModel()
            {
                Departamento = depart.Departamento
            };
            FlexDepart.BindingContext = ListDepart;
            Title = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CarregarDepart();
        }

        private void CarregarDepart()
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread( async () =>
                {
                    DepartamentoDB db = new DepartamentoDB();

                    try
                    {
                        var departs = await db.ListarDepartamentos();
                        foreach (var dep in departs)
                        {
                            ListDepart.Add(new DepartamentoViewModel()
                            {
                                Departamento = dep
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Erro", string.Format("{0}\n{1}", ex.Message, ex.StackTrace), "OK");
                    }
                });

            });
        }

    }
}