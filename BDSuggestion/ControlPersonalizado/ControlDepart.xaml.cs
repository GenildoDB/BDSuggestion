using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDSuggestion.ControlPersonalizado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ControlDepart : ContentView
	{

		public static readonly BindableProperty TextoProperty =
		 BindableProperty.Create(nameof(Texto), typeof(string), typeof(ControlDepart), null, propertyChanged: OnTextChanged);

		public string Texto
		{
			get => (string)GetValue(TextoProperty);
			set => SetValue(TextoProperty, value);
		}

		private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ControlDepart)bindable;

			var value = (string)newValue;

			control.LblTexto.Text = value;
		}

		public ControlDepart ()
		{
			InitializeComponent ();
			
		}

		public ControlDepart(string TextoDepartamento)
		{
			InitializeComponent();
			
			Texto = TextoDepartamento;
			//BindingContext = Texto;
		}
	}
}