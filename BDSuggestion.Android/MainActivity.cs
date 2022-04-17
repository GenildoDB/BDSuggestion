using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
//using Plugin.InputKit;
using System.IO;
using Android.Views;
//using FFImageLoading.Forms.Platform;
//using FFImageLoading.Svg.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Essentials;

namespace BDSuggestion.Droid
{
    [Activity(Label = "BDSuggestion", Icon = "@mipmap/icon",  MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            string l = Path.Combine(FileSystem.AppDataDirectory, "dbsugest.db");
            var db = new BDSuggestion.ControlPersonalizado.DBHelper(this);
            db.CriaSQLiteDB(l, 2131623936);


            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#5412dc"));

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}