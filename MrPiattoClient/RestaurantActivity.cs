
using Android.App;
using Android.Support.V4.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Runtime;
using MrPiattoClient.WebService;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Content;
using Android.Gms.Maps;
using RestSharp;

namespace MrPiattoClient
{
    [Activity(Label = "RestaurantActivity")]
    public class RestaurantActivity : AppCompatActivity, IOnMapReadyCallback
    {
        private MapView mapView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            InitListeners();
            InitMap(savedInstanceState);
            InitToolbar();
        }
        protected override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
        }
        protected override void OnStart()
        {
            base.OnStart();
            mapView.OnStart();
        }
        protected override void OnStop()
        {
            base.OnStop();
            mapView.OnStop();
        }
        protected override void OnPause()
        {
            mapView.OnPause();
            base.OnPause();
        }
        protected override void OnDestroy()
        {
            mapView.OnDestroy();
            base.OnDestroy();
        }
        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView.OnLowMemory();
        }
        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            base.OnSaveInstanceState(outState, outPersistentState);
            Bundle mapViewBundle = outState.GetBundle("MapViewBundleKey");
            if (mapViewBundle == null)
            {
                mapViewBundle = new Bundle();
                outState.PutBundle("MapViewBundleKey", mapViewBundle);
            }
            mapView.OnSaveInstanceState(mapViewBundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_restaurant, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
            ToastLength.Short).Show();
            
            switch(item.ItemId)
            {
                case Resource.Id.shareRestaurant:

                    return true;

                case Resource.Id.addToFavorite:
                    Intent intent2 = new Intent(this, typeof(VisitedActivity));
                    StartActivity(intent2);
                    return true;

                case Resource.Id.helperItem:
                    Intent intent3 = new Intent(this, typeof(HomeActivity));
                    StartActivity(intent3);
                    return true;


                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        
        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.AddMarker(new Android.Gms.Maps.Model.MarkerOptions().SetPosition(new Android.Gms.Maps.Model.LatLng(0, 0)));
        }

        public void InitListeners()
        {
            Button buttonSeeMorePhotos = FindViewById<Button>(Resource.Id.buttonSeeMorePhotos);
            buttonSeeMorePhotos.Click += delegate
            {
                Intent intent = new Intent(this, typeof(RestaurantPhotosActivity));
                StartActivity(intent);
            };
            Button buttonSeeMoreRatings = FindViewById<Button>(Resource.Id.buttonSeeMoreRatings);
            buttonSeeMoreRatings.Click += delegate
            {
                Intent intent = new Intent(this, typeof(RestaurantRatingsActivity));
                StartActivity(intent);
            };
            Button buttonCall = FindViewById<Button>(Resource.Id.buttonCall);
            buttonCall.Click += delegate
            {
                Intent intent = new Intent(Intent.ActionDial);
                intent.SetData(Android.Net.Uri.Parse("tel:" + buttonCall.Text));
                StartActivity(intent);
            };
            Button buttonBookATable = FindViewById<Button>(Resource.Id.btnBookATable);
            buttonBookATable.Click += delegate
            {
                Intent intent = new Intent(this, typeof(BookingActivity));
                StartActivity(intent);
            };
        }
        public void InitMap(Bundle savedInstanceState)
        {
            Bundle mapViewBundle = null;
            if (savedInstanceState != null)
            {
                mapViewBundle = savedInstanceState.GetBundle("MapViewBundleKey");
            }
            mapView = FindViewById<MapView>(Resource.Id.googleMap);
            mapView.OnCreate(mapViewBundle);
            mapView.GetMapAsync(this);
        }
        public void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            CollapsingToolbarLayout collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsingToolbar);
            AppBarLayout appBarLayout = FindViewById<AppBarLayout>(Resource.Id.appBar);
            toolbar.Title = "";
            SetSupportActionBar(toolbar);
            var isShow = false;
            var scrollRange = -1;

            appBarLayout.OffsetChanged += (sender, args) =>
            {
                if (scrollRange == -1)
                    scrollRange = appBarLayout.TotalScrollRange;
                if (scrollRange + args.VerticalOffset == 0)
                {
                    collapsingToolbar.Title = "Mr. Piatto Restaurant";
                    isShow = true;
                }
                else if (isShow)
                {
                    collapsingToolbar.Title = string.Empty;
                    isShow = false;
                }
            };
        }

        
    }
}