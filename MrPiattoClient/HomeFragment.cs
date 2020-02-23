using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;

namespace MrPiattoClient
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        List<Restaurant> main = new List<Restaurant>()
        {
            new Restaurant(5, "Mr. Piatto Restaurant", "Vista a la Campina 5414, Cerro del Tesoro, 45608, Tlaquepaque, Jal.", "TAILANDESA"),
            new Restaurant(5, "Tacos de DonPerro", "Nueva Escocia 12641, Afuera del CETI, 45608, Gudalajara, Jal.", "DESCONOCIDO"),
            new Restaurant(4, "Fonda de doña chona", "Vista a la Campina 5414, Cerro del Tesoro, 45608", "MEXICANA")
        };
        List<Cuisine> cuisines = new List<Cuisine>()
        {
            new Cuisine("Mexicana"),
            new Cuisine("Francesa"),
            new Cuisine("Italiana"),
            new Cuisine("Comida rápida")
        };
        RecyclerView recycler, recyclerNear, recyclerCuisine;
        RecyclerViewMainAdapter adapter;
        RecyclerViewCuisineAdapter adapterCuisine;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_main, container, false);
            recycler = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerViewMainFavorite);
            recycler.SetLayoutManager(new LinearLayoutManager(rootView.Context, LinearLayoutManager.Horizontal, false));
            recycler.SetItemAnimator(new DefaultItemAnimator());

            recyclerNear = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerViewMainNear);
            recyclerNear.SetLayoutManager(new LinearLayoutManager(rootView.Context, LinearLayoutManager.Horizontal, false));
            recyclerNear.SetItemAnimator(new DefaultItemAnimator());

            recyclerCuisine = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerViewMainCuisine);
            recyclerCuisine.SetLayoutManager(new LinearLayoutManager(rootView.Context, LinearLayoutManager.Horizontal, false));
            recyclerCuisine.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewMainAdapter(main);
            adapterCuisine = new RecyclerViewCuisineAdapter(cuisines);
            recycler.SetAdapter(adapter);
            recyclerNear.SetAdapter(adapter);
            recyclerCuisine.SetAdapter(adapterCuisine);

            ImageView profilePic = rootView.FindViewById<ImageView>(Resource.Id.imageMainProfile);
            profilePic.Click += delegate
            {
                Intent intent = new Intent(rootView.Context, typeof(AboutMeActivity));
                StartActivity(intent);
            };


            return rootView;
        }


        public static HomeFragment NewInstance()
        {
            return new HomeFragment { Arguments = new Bundle() };
        }

    }
    
}
