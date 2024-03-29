﻿using System;
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
using MrPiattoClient.Resources.utilities;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MrPiattoClient
{
    [Activity(Label = "FavoriteActivity")]
    public class FragmentFavorite : Fragment
    {
        APICaller API = new APICaller();
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
            if(!Preferences.Get("boolFavorite", false))
            {
                Preferences.Set("JSONFavorite", API.GetFavoritesJSON(Preferences.Get("idUser", 0)));
                Preferences.Set("boolFavorite", true);
            }
            List<CompleteRestaurant> restaurants = JsonConvert.DeserializeObject<List<CompleteRestaurant>>
                (Preferences.Get("JSONFavorite", null));

            recycler = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewFavorite);
            recycler.SetLayoutManager(new LinearLayoutManager(view.Context));
            recycler.SetItemAnimator(new DefaultItemAnimator());
            adapter = new RecyclerViewFavoriteAdapter(restaurants, view.Context);
            recycler.SetAdapter(adapter);
        }
        public static FragmentFavorite NewInstance()
        {
            return new FragmentFavorite { Arguments = new Bundle() };
        }
    }
}