﻿using System;
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
    [Activity(Label = "ActivityHome")]
    public class ActivityHome : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bottomNavigation);

            BottomNavigationView bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottomNavMain);
            bottomNavigation.NavigationItemSelected += FragmentListener;

            LoadFragment(Resource.Id.itemHome);
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