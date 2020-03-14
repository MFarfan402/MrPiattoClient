using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityVerifierMain")]
    public class ActivityVerifierMain : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_mainVerifier);
            BottomNavigationView bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottomNavMainVerifier);
            bottomNavigation.InflateMenu(Resource.Menu.menu_navigationV);
            bottomNavigation.NavigationItemSelected += FragmentListener;

            LoadFragment(Resource.Id.itemAdd);
        }

        private void LoadFragment(int item)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (item)
            {
                case Resource.Id.itemAdd:
                    fragment = FragmentVerifierNewRestaurant.NewInstance();
                    break;
                case Resource.Id.itemTracking:
                    fragment = FragmentSearchVerifier.NewInstance();
                    break;
                case Resource.Id.itemBadWords:
                    fragment = FragmentCommentVerifier.NewInstance();
                    break;
            }
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.frameMainContentVerifier, fragment)
                .Commit();
            if (fragment == null)
                return;
        }

        private void FragmentListener(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
    }
}