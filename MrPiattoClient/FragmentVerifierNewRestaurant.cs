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
using Fragment = Android.Support.V4.App.Fragment;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;
using MrPiattoClient.Models;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    public class FragmentVerifierNewRestaurant : Fragment
    {
        APICaller API = new APICaller();
        RecyclerView recycler;
        RecyclerViewNewAdapter adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_addRestaurant, container, false);
            recycler = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerAddRestaurant);
            recycler.SetLayoutManager(new LinearLayoutManager(rootView.Context));
            recycler.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewNewAdapter(API.GetNewRestaurants(), rootView.Context);
            recycler.SetAdapter(adapter);
            return rootView;
        }

        public static FragmentVerifierNewRestaurant NewInstance()
        {
            return new FragmentVerifierNewRestaurant { Arguments = new Bundle() };
        }
    }

    
}