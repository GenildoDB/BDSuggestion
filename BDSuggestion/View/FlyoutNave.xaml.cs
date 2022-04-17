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
    public partial class FlyoutNave : FlyoutPage
    {
        public FlyoutNave()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            FlyoutNaveFlyoutMenuItem item = e.SelectedItem as FlyoutNaveFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);

            if (!item.Title.Equals("Home"))
                page.Title = item.Title;

            IsPresented = false;
            FlyoutPage.ListView.SelectedItem = null;

            if (Navi.Navigation.NavigationStack.Last() is Home && item.Title.Equals("Home"))
                return;

          
            Navi.Navigation.PushAsync(page);

           
        }
    }
}