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

namespace MrPiattoClient
{
    [Activity(Label = "FavoriteActivity")]
    public class FavoriteActivity : AppCompatActivity
    {
        List<Restaurant> restaurants = new List<Restaurant>()
        {
            new Restaurant(5, "Mr. Piatto Restaurant", "Vista a la Campina 5414, Cerro del Tesoro, 45608, Tlaquepaque, Jal.", "TAILANDESA"),
            new Restaurant(5, "Tacos de DonPerro", "Nueva Escocia 12641, Afuera del CETI, 45608, Gudalajara, Jal.", "DESCONOCIDO"),
            new Restaurant(4, "Fonda de doña chona", "Vista a la Campina 5414, Cerro del Tesoro, 45608", "MEXICANA")
        };
        RecyclerView recycler;
        RecyclerViewFavoriteAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_favorite);

            InitToolbar();
            PutFavoriteRestaurants();
        }

        private void PutFavoriteRestaurants()
        {
            //recyclerViewFavorite
            recycler = FindViewById<RecyclerView>(Resource.Id.recyclerViewFavorite);
            recycler.SetLayoutManager(new LinearLayoutManager(this));
            recycler.SetItemAnimator(new DefaultItemAnimator());
            adapter = new RecyclerViewFavoriteAdapter(restaurants);
            recycler.SetAdapter(adapter);
            
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarFavorite);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Favoritos";
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

//MrPiattoRestaurant/adapters/
//helper
//expandable recyclerview