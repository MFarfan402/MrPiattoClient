using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityHome")]
    public class ActivityHome : FragmentActivity
    {
        APICaller API = new APICaller();
        int idUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bottomNavigation);
            idUser = Preferences.Get("idUser", 0);

            if (!Preferences.Get("boolFavorite", false))
            {
                Preferences.Set("JSONFavorite", API.GetFavoritesJSON(idUser));
                Preferences.Set("boolFavorite", true);
            }

            if (!Preferences.Get("boolReservation", false))
            {
                Preferences.Set("JSONReservation", API.GetFavoritesJSON(idUser));
                Preferences.Set("boolReservation", true);
            }

            BottomNavigationView bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottomNavMain);
            bottomNavigation.NavigationItemSelected += FragmentListener;

            LoadFragment(Resource.Id.itemHome);

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.sideMenu);
            navigationView.NavigationItemSelected += ActivityCallback;
        }

        private void ActivityCallback(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            InitActivity(e.MenuItem.ItemId);
        }

        private void InitActivity(int itemId)
        {
            Intent intent;
            switch (itemId)
            {
                case Resource.Id.itemVisited:
                    intent = new Intent(this, typeof(VisitedActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.itemPersonalData:
                    intent = new Intent(this, typeof(ActivityMyProfile));
                    StartActivity(intent);
                    break;
                case Resource.Id.itemStrikes:
                    intent = new Intent(this, typeof(ActivityStrikes));
                    intent.PutExtra("idUser", idUser);
                    StartActivity(intent);
                    break;
                case Resource.Id.itemNotifications:
                    intent = new Intent(this, typeof(ActivityNotification));
                    StartActivity(intent);
                    break;
                case Resource.Id.itemRegisterRestaurant:
                    intent = new Intent(this, typeof(ActivityJoinUs));
                    StartActivity(intent);
                    break;
                case Resource.Id.itemLogOut:
                    Preferences.Set("boolUser", false);
                    Preferences.Set("boolReservation", false);
                    Preferences.Set("boolFavorite", false);
                    Preferences.Set("boolNFPush", true);
                    Preferences.Set("boolNFMail", true);
                    intent = new Intent(this, typeof(ActivityLogIn));
                    StartActivity(intent);
                    Finish();
                    break;
                    

            }
        }

        private void FragmentListener(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        private void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            
            switch (id)
            {
                case Resource.Id.itemHome:
                        fragment = FragmentHome.NewInstance(this);
                    break;
                case Resource.Id.itemSearch:
                    fragment = FragmentSearch.NewInstance();
                    break;
                case Resource.Id.itemReservations:
                    fragment = FragmentMyReservations.NewInstance();
                    break;
                case Resource.Id.itemFavorite:
                    fragment = FragmentFavorite.NewInstance();
                    break;
            }
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.frameMainContent, fragment)
                .Commit();
            if (fragment == null)
            return;
        }
    }
}