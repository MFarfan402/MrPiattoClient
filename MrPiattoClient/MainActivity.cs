using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using MrPiattoClient.MrPiattoDB;
using MrPiattoClient.WebService;
using MrPiattoClient.MrPiattoDB;

namespace MrPiattoClient
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView label1, label2, label3;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            Button button3 = FindViewById<Button>(Resource.Id.button3);
            Button button4 = FindViewById<Button>(Resource.Id.button4);

            label1 = FindViewById<TextView>(Resource.Id.label1);
            label2 = FindViewById<TextView>(Resource.Id.label2);
            label3 = FindViewById<TextView>(Resource.Id.label3);

            button1.Click += delegate { Insert(); };
            button2.Click += delegate { Delete(); };
            button3.Click += delegate { Update(); };
            button4.Click += delegate { Read(); };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void Insert()
        {
            WebService1 webService = new WebService1();
            webService.InsertRestaurant("aaaaaaa","bbbbbb", false);
        }

        public void Delete()
        {
            WebService1 webService = new WebService1();
            webService.DeleteRestaurant(1009);
        }

        public void Update()
        {
            WebService1 webService = new WebService1();
           
        }

        public void Read()
        {
            WebService.Restaurant restaurant = new WebService.Restaurant();
            WebService1 webService = new WebService1();
            restaurant = webService.ReadRestaurant(1009);
            label1.Text = restaurant.Mail;
            label2.Text = restaurant.Password;
            label3.Text = restaurant.Confirmation.ToString();
        }
    }
}