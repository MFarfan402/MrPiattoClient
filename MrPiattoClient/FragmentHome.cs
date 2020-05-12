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
using MrPiattoClient.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    public class FragmentHome : Android.Support.V4.App.Fragment
    {
        public Context contextFragment;
        APICaller API = new APICaller();
        List<Restaurant> main = new List<Restaurant>();
        List<Restaurant> fav = new List<Restaurant>();
        RecyclerView recycler, recyclerNear, recyclerCuisine;
        RecyclerViewMainAdapter adapter, adapterNear;
        RecyclerViewCuisineAdapter adapterCuisine;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (!Preferences.Get("boolFavorite", false))
            {
                Preferences.Set("JSONFavorite", API.GetFavoritesJSON(Preferences.Get("idUser", 0)));
                Preferences.Set("boolFavorite", true);
            }
            List<CompleteRestaurant> favorites = JsonConvert.DeserializeObject<List<CompleteRestaurant>>
                (Preferences.Get("JSONFavorite", null));


            List<CompleteRestaurant> mainR = JsonConvert.DeserializeObject<List<CompleteRestaurant>>
                (Preferences.Get("JSONRes", null));

            List<IdcategoriesNavigation> categories = API.GetCategories();

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

            adapter = new RecyclerViewMainAdapter(favorites, rootView.Context);
            adapterNear = new RecyclerViewMainAdapter(mainR, rootView.Context);
            adapterCuisine = new RecyclerViewCuisineAdapter(categories);
            recycler.SetAdapter(adapter);
            recyclerNear.SetAdapter(adapterNear);
            recyclerCuisine.SetAdapter(adapterCuisine);

            return rootView;
        }


        public static FragmentHome NewInstance(Context context)
        {
            return new FragmentHome { Arguments = new Bundle() };
        }

    }
    
}
