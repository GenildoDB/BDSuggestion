using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDSuggestion.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutNaveFlyout : ContentPage
    {
        public ListView ListView;

        public FlyoutNaveFlyout()
        {
            InitializeComponent();

            BindingContext = new FlyoutNaveFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class FlyoutNaveFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FlyoutNaveFlyoutMenuItem> MenuItems { get; set; }

            public FlyoutNaveFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FlyoutNaveFlyoutMenuItem>(new[]
                {
                    new FlyoutNaveFlyoutMenuItem { Id = 0, Title = "Home", TargetType = typeof(Home)},
                    new FlyoutNaveFlyoutMenuItem { Id = 1, Title = "Cadastro de Sugestão", TargetType = typeof(PageCadSugestao)},
                    new FlyoutNaveFlyoutMenuItem { Id = 2, Title = "Cadastro de Departamento", TargetType = typeof(PageCadDepart) },
                    new FlyoutNaveFlyoutMenuItem { Id = 3, Title = "Lista de Sugestões", TargetType = typeof(PageListSugestao) },
                    new FlyoutNaveFlyoutMenuItem { Id = 4, Title = "Tipos de Departamentos", TargetType = typeof(PageListDepart) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}