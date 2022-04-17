using BDSuggestion.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BDSuggestion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /*
         * O CAMPO NOME DO COLABORADOR NESSA PÁGINA PODE SER TROCADO POR LOGIN E SENHA EM UM APLICATIVO DE PRODUÇÃO
         */

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryColaborador.Text))
            {
                await DisplayAlert("", "É necessário informa o seu nome!", "OK");
                return;
            }
            App.Colaborador = EntryColaborador.Text;
            App.Current.MainPage = new FlyoutNave();
        }
    }
}
