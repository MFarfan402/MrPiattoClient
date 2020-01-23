using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Runtime;
using MrPiattoClient.WebService;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Content;

namespace MrPiattoClient
{
   [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button buttonSeeMorePhotos = FindViewById<Button>(Resource.Id.buttonSeeMorePhotos);
            buttonSeeMorePhotos.Click += delegate
            {
                Intent intent = new Intent(this, typeof(RestaurantPhotosActivity));
                StartActivity(intent);
            };

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            CollapsingToolbarLayout collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsingToolbar);
            AppBarLayout appBarLayout = FindViewById<AppBarLayout>(Resource.Id.appBar);

            collapsingToolbar.SetTitle("Esta es la prueba");
            collapsingToolbar.SetExpandedTitleColor(GetColor(Resource.Color.mtrl_btn_transparent_bg_color));
            collapsingToolbar.SetCollapsedTitleTextColor(Android.Resource.Color.Black);

            SetSupportActionBar(toolbar);
            toolbar.InflateMenu(Resource.Menu.menu_restaurant);
            toolbar.MenuItemClick += (sender, e) =>
            {
                Toast.MakeText(this, e.Item.TitleFormatted, ToastLength.Long).Show();
            };
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        /*

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_restaurant, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
        */
    }
}