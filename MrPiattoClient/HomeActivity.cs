using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MrPiattoClient
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LoadFragment();
        }

        private void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch(id)
            {
                case Resource.Id.itemHome:
                    fragment = Fragment1.NewInstance();
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

            SupportFragmentManager.BeginTransaction()
            .Replace(Resource)
        }

    }
}