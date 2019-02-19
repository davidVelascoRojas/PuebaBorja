namespace Lands.Droid
{
    using Acr.UserDialogs;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using FFImageLoading.Forms.Droid;
    using System.IO;

    [Activity(Label = "BORJA", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            UserDialogs.Init(this);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init(true);

            string nombreArchivo = "bd_PRUEBA.sqlite";
            string rutaCarpeta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            LoadApplication(new App(rutaCompleta));
        }
    }
}