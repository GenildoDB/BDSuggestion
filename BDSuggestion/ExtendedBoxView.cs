using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BDSuggestion
{
    public class ExtendedBoxView : BoxView
    {
        /// <summary>
        /// Respresents the background color of the button.
        /// </summary>
        public static readonly BindableProperty BorderRadiusProperty = BindableProperty.Create("BorderRadius", typeof(double), typeof(ExtendedBoxView), 0.0);//  Create<ExtendedBoxView, double>(p => p.BorderRadius, 0);  //BindableProperty.Create<ExtendedBoxView, double>(p => p.BorderRadius, 0);

        public double BorderRadius
        {
            get { return (double)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static readonly BindableProperty StrokeProperty = BindableProperty.Create("Stroke", typeof(Color), typeof(ExtendedBoxView), Color.Transparent);
          //  BindableProperty.Create<ExtendedBoxView, Color>(p => p.Stroke, Color.Transparent);

        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create("StrokeThickness", typeof(double), typeof(ExtendedBoxView), 0.0);
            //BindableProperty.Create<ExtendedBoxView, double>(p => p.StrokeThickness, 0);

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }
    }
}
