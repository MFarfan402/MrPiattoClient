using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.RecyclerView;
using Android.Views;
using Android.Widget;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;

namespace MrPiattoClient
{
    [Activity(Label = "RestaurantPhotosActivity")]
    public class RestaurantPhotosActivity : Activity
    {
        //Ths is the hardcoding for the strings.
        List<string> data = new List<string>() 
        {
            "http://192.168.100.207/FileServer/grid1.jpg",
            "http://192.168.100.207/FileServer/grid2.jpg",
            "http://192.168.100.207/FileServer/grid3.jpg",
            "http://192.168.100.207/FileServer/grid4.jpg",
            "http://192.168.100.207/FileServer/grid5.jpg",
        };
        RecyclerView recyclerView;
        RecyclerViewAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_photos);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewPhotos);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewAdapter(data);
            recyclerView.SetAdapter(adapter);

            // Ask for the data to the web service. Then pass the URL/name of each of the elements here.

        }
    }
}