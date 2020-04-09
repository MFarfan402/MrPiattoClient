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
using Fragment = Android.Support.V4.App.Fragment;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;
using MrPiattoClient.Models;

namespace MrPiattoClient
{
    [Activity(Label = "FavoriteActivity")]
    public class FragmentFavorite : Fragment
    {
        List<Restaurant> restaurants = new List<Restaurant>()
        {
            new Restaurant(5, "Mr. Piatto Restaurant", "Vista a la Campina 5414, Cerro del Tesoro, 45608, Tlaquepaque, Jal.", "TAILANDESA"),
            new Restaurant(5, "Tacos de DonPerro", "Nueva Escocia 12641, Afuera del CETI, 45608, Gudalajara, Jal.", "DESCONOCIDO"),
            new Restaurant(4, "Fonda de doña chona", "Vista a la Campina 5414, Cerro del Tesoro, 45608", "MEXICANA")
        };
        RecyclerView recycler;
        RecyclerViewFavoriteAdapter adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_favorite, container, false);
            PutFavoriteRestaurants(view);
            return view;
        }

        private void PutFavoriteRestaurants(View view)
        {
            recycler = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewFavorite);
            recycler.SetLayoutManager(new LinearLayoutManager(view.Context));
            recycler.SetItemAnimator(new DefaultItemAnimator());
            adapter = new RecyclerViewFavoriteAdapter(restaurants);
            recycler.SetAdapter(adapter);
        }
        public static FragmentFavorite NewInstance()
        {
            return new FragmentFavorite { Arguments = new Bundle() };
        }
    }
}