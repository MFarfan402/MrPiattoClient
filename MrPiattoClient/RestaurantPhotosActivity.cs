using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.Activity;
using MrPiattoClient.Resources.adapter;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AndroidX.RecyclerView.Widget;

namespace MrPiattoClient
{
    [Activity(Label = "RestaurantPhotosActivity")]
    public class RestaurantPhotosActivity : AppCompatActivity
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

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarPhotos);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Fotos";
            PutPhotosOnScreen();
            // Ask for the data to the web service. Then pass the URL/name of each of the elements here.

        }

        public void PutPhotosOnScreen()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewPhotos);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewAdapter(data);
            recyclerView.SetAdapter(adapter);
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