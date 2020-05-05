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
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;
using MrPiattoClient.Models;
using Xamarin.Essentials;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    [Activity(Label = "VisitedActivity")]
    public class VisitedActivity : AppCompatActivity
    {
        APICaller API = new APICaller();
        RecyclerView recycler;
        RecyclerViewVisitedAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_visited);

            InitToolbar();
            PutFavoriteRestaurants();
        }
        private void PutFavoriteRestaurants()
        {
            List<CompleteRestaurant> completeRestaurants = API.GetVisitedRestaurants(Preferences.Get("idUser", 0));
            List<Restaurant> restaurants = new List<Restaurant>();
            foreach(var r in completeRestaurants)
                restaurants.Add(new Restaurant(r.idrestaurant, r.score, r.name, r.address, r.idcategoriesNavigation.category));
            recycler = FindViewById<RecyclerView>(Resource.Id.recyclerViewVisited);
            recycler.SetLayoutManager(new LinearLayoutManager(this));
            recycler.SetItemAnimator(new DefaultItemAnimator());
            adapter = new RecyclerViewVisitedAdapter(restaurants, this);
            recycler.SetAdapter(adapter);

        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarVisited);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Visitados";
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}