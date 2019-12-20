using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using MrPiattoClient.MrPiattoDB;

namespace MrPiattoClient
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += delegate
            {
                consulta();
            };
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void consulta()
        {
            Restaurant restaurant = new Restaurant();
            restaurant.Mail = "@mrpiatto.com";
            restaurant.Password = "123";
            restaurant.Confirmation = false;
            using (var db = new MrPiattoContext())
            {
                db.Restaurant.Add(restaurant);
                db.SaveChanges();
            }
        }
    }
}