using BDSuggestion.Services;
using BDSuggestion.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BDSuggestion.ViewModel
{
   public class DepartamentoAddViewModel : DepartamentoViewModel
    {
        public ICommand Add { get; set; }

        public DepartamentoAddViewModel()
        {
            Add = new Command(AddDepart);
        }

        private async void AddDepart()
        {
            try
            {
                if (!await ValidarCampos())
                {
                    return;
                }

                DepartamentoDB db = new DepartamentoDB();

                var depart = await db.GetDepart(Departamento.Nome);
                if (depart != null && depart.Id > 0)
                {
                    await App.Current.MainPage.DisplayAlert("", "Já existe um departamento com esse nome!", "OK");
                    return;
                }

                int result = await db.AddUpdate(Departamento);

                if (result > 0)
                {
                    await App.Current.MainPage.DisplayAlert("Departamentos", "Departamento salvo com sucesso!", "OK");            
                    await ((FlyoutNave)App.Current.MainPage).Navi.Navigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro ao salvar a sugestão", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", string.Format("{0}\n{1}", ex.Message, ex.StackTrace), "OK");
            }
        }

        private async ValueTask<bool> ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(Departamento.Nome))
            {
                await App.Current.MainPage.DisplayAlert("", "O campo 'Nome do departamento' não pode ficar vazio!", "OK");
                return false;
            }
            
            return true;
        }
    }
}
