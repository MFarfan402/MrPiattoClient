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
using AndroidX.RecyclerView.Widget;
using MrPiattoClient.Resources.adapter;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    public class FragmentSearchVerifier : Android.Support.V4.App.Fragment
    {
        APICaller API = new APICaller();
        RecyclerView recycler;
        RVInactiveAdapter adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_searchVerifier, container, false);

            recycler = view.FindViewById<RecyclerView>(Resource.Id.recyclerWarningRestaurants);
            recycler.SetLayoutManager(new LinearLayoutManager(view.Context));
            recycler.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RVInactiveAdapter(view.Context, API.GetInactiveRestaurants());
            recycler.SetAdapter(adapter);
            return view;
        }

        public static FragmentSearchVerifier NewInstance()
        {
            return new FragmentSearchVerifier { Arguments = new Bundle() };
        }
    }
}