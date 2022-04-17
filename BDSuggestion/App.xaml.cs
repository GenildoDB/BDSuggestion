using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDSuggestion
{
    public partial class App : Application
    {

        public static string Colaborador { get; set; }
        private static MainEntitie Entitie_;
        public static MainEntitie Entitie
        {
            get
            {
                if (Entitie_ == null)
                    Entitie_ = new MainEntitie();

                return Entitie_;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            Entitie.Database.OpenConnection();

            if (await Entitie.Departamentos.AsNoTracking().CountAsync(p => p.Nome.Equals("Financeiro")) == 0 &&
                await Entitie.Departamentos.AsNoTracking().CountAsync(p => p.Nome.Equals("Almoxarifado")) == 0)
            {
                await Entitie.Departamentos.AddAsync(new Models.Departamentos() { Nome = "Financeiro" });
                await Entitie.Departamentos.AddAsync(new Models.Departamentos() { Nome = "Almoxarifado" });
                
                Entitie.SaveChanges();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
