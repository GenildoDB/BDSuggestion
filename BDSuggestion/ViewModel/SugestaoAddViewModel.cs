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
    public class SugestaoAddViewModel : SugestaoViewModel
    {
        public ICommand Add { get; set; }

        public SugestaoAddViewModel()
        {
            Add = new Command(AddSugestao);
        }


        private async ValueTask<bool> ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(Sugestao.Colaborador))
            {
                await App.Current.MainPage.DisplayAlert("", "O campo 'Colaborador' não pode ficar vazio!", "OK");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Sugestao.Departamento))
            {
                await App.Current.MainPage.DisplayAlert("", "O campo 'Departamento' não pode ficar vazio!", "OK");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Sugestao.Sugestao))
            {
                await App.Current.MainPage.DisplayAlert("", "O campo 'Sugestão' não pode ficar vazio!", "OK");
                return false;
            }

            return true;
        }

        private async void AddSugestao()
        {
            try
            {
                if (!await ValidarCampos())
                {
                    return;
                }

                SugestaoDB db = new SugestaoDB();
                int result = await db.AddUpdate(Sugestao);

                if (result > 0)
                {
                    await App.Current.MainPage.DisplayAlert("Sugestão", "Sugestão salva com sucesso!", "OK");
                    App.Colaborador = Sugestao.Colaborador;
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
    }
}
