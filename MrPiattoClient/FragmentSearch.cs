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
using SearchView = AndroidX.AppCompat.Widget.SearchView;

namespace MrPiattoClient
{
    public class FragmentSearch : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_search, container, false);
            SearchView searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
            searchView.QueryHint = "Busca tu restaurante...";
            return view;
        }
        public static FragmentSearch NewInstance()
        {
            return new FragmentSearch { Arguments = new Bundle() };
        }
    }
}