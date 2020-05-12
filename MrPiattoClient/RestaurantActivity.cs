using Android.App;
using Android.Support.V4.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Content;
using Android.Gms.Maps;
using RestSharp;
using System;
using System.Threading.Tasks;
using MrPiattoClient.Resources.utilities;
using MrPiattoClient.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using Android.Gms.Maps.Model;
using Xamarin.Essentials;

namespace MrPiattoClient
{
    [Activity(Label = "RestaurantActivity")]
    public class RestaurantActivity : AppCompatActivity, IOnMapReadyCallback
    {
        APICaller API = new APICaller();
        private MapView mapView;
        int idRestaurant;
        CompleteRestaurant restaurant;
        List<int> bars;
        List<float> ratings;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            idRestaurant = Intent.GetIntExtra("idRestaurant", 0);
            InitListeners();
            InitMap(savedInstanceState);
            InflateMainData();
        }

        private void InflateMainData()
        {
            restaurant = JsonConvert.DeserializeObject<CompleteRestaurant>(
                Intent.GetStringExtra("mainInfo"));
            idRestaurant = restaurant.idrestaurant;

            TextView restaurantName = FindViewById<TextView>(Resource.Id.restaurantName);
            TextView restaurantLocation = FindViewById<TextView>(Resource.Id.restaurantLocation);
            TextView restaurantCuisine = FindViewById<TextView>(Resource.Id.restaurantCuisine);
            RatingBar ratingBar = FindViewById<RatingBar>(Resource.Id.restaurantRating);
            restaurantName.Text = restaurant.name;
            restaurantLocation.Text = restaurant.address;
            restaurantCuisine.Text = restaurant.idcategoriesNavigation.category;
            ratingBar.Rating = restaurant.score;

            Button call = FindViewById<Button>(Resource.Id.buttonCall);
            TextView informationPrice = FindViewById<TextView>(Resource.Id.informationPrice);
            TextView informationView = FindViewById<TextView>(Resource.Id.informationFood);
            TextView informationPayment = FindViewById<TextView>(Resource.Id.informationPayment);
            TextView informationDress = FindViewById<TextView>(Resource.Id.informationDress);
            TextView description = FindViewById<TextView>(Resource.Id.informationComment);
            call.Text = restaurant.phone;
            informationPrice.Text = $"${restaurant.price} MXN o más";
            informationView.Text = restaurant.idcategoriesNavigation.category;
            informationPayment.Text = restaurant.idpaymentNavigation.paymentOption;
            informationDress.Text = restaurant.dress;
            description.Text = restaurant.description;

            TextView schedule = FindViewById<TextView>(Resource.Id.informationHour);
            schedule.Text = API.GetSchedule(idRestaurant);

            InitToolbar(restaurant.name);

            bars = API.GetBars(idRestaurant);
            ratings = API.GetRatings(idRestaurant);
            TextView generalRating = FindViewById<TextView>(Resource.Id.generalRating);
            RatingBar rating = FindViewById<RatingBar>(Resource.Id.restaurantRatingSmall);
            ProgressBar pbar1 = FindViewById<ProgressBar>(Resource.Id.pbar1);
            ProgressBar pbar2 = FindViewById<ProgressBar>(Resource.Id.pbar2);
            ProgressBar pbar3 = FindViewById<ProgressBar>(Resource.Id.pbar3);
            ProgressBar pbar4 = FindViewById<ProgressBar>(Resource.Id.pbar4);
            ProgressBar pbar5 = FindViewById<ProgressBar>(Resource.Id.pbar5);
            rating.Rating = ratings[3];
            generalRating.Text = ratings[3].ToString();
            pbar1.Progress = bars[0];
            pbar2.Progress = bars[1];
            pbar3.Progress = bars[2];
            pbar4.Progress = bars[3];
            pbar5.Progress = bars[4];

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
            switch(item.ItemId)
            {
                case Resource.Id.addToFavorite:
                    API.AddToFavoriteAsync(idRestaurant);
                    Preferences.Set("boolFavorite", false);
                    Toast.MakeText(this, "Restaurante añadido a favoritos", ToastLength.Short).Show();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        
        public void OnMapReady(GoogleMap googleMap)
        {
            LatLng loc = new LatLng(restaurant.lat, restaurant.@long);
            googleMap.AddMarker(new MarkerOptions().
                SetPosition(loc));
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(loc);
            builder.Zoom(18);
            builder.Tilt(65);

            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            googleMap.MoveCamera(cameraUpdate);
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
                Intent intent = new Intent(this, typeof(ActivityRestaurantRatings));
                intent.PutExtra("idRestaurant", idRestaurant);
                intent.PutExtra("bars", JsonConvert.SerializeObject(bars));
                intent.PutExtra("ratings", JsonConvert.SerializeObject(ratings));
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
                intent.PutExtra("idRestaurant", idRestaurant);
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
        public void InitToolbar(string name)
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
                    collapsingToolbar.Title = name;
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