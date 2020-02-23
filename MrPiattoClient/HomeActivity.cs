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

namespace MrPiattoClient
{
    
    [Activity(MainLauncher = true)]
    public class HomeActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bottomNavigation);

            BottomNavigationView bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottomNavMain);
            bottomNavigation.NavigationItemSelected += FragmentListener;

            InitFragments();
            LoadFragment(Resource.Id.itemHome);
        }

        private void InitFragments()
        {
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
                    fragment = HomeFragment.NewInstance();
                    SupportFragmentManager.BeginTransaction()
                        .Replace(Resource.Id.frameMainContent, fragment)
                        .Commit();
                    break;
                case Resource.Id.itemSearch:
                    
                    break;
                case Resource.Id.itemReservations:
                    break;
                case Resource.Id.itemFavorite:
                    break;
            }

            if (fragment == null)
            return;

            
        }
    }
}