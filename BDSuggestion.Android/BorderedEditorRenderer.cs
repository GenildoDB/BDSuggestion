﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BDSuggestion.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(BorderedEditorRenderer))]
namespace BDSuggestion.Droid
{
    public class BorderedEditorRenderer : EditorRenderer
    {

        public BorderedEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;

                var lp = new MarginLayoutParams(Control.LayoutParameters);
                lp.SetMargins(0, 0, 0, 0);
                LayoutParameters = lp;
                Control.LayoutParameters = lp;
                Control.SetPadding(10, 3, 10, 3);
                SetPadding(0, 0, 0, 0);
            }
        }
    }
}