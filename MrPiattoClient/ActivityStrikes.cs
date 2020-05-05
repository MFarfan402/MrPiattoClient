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
using AndroidX.RecyclerView.Widget;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using MrPiattoClient.Models;
using MrPiattoClient.Resources.adapter;
using MrPiattoClient.Resources.utilities;
using Xamarin.Essentials;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityStrikes")]
    public class ActivityStrikes : AppCompatActivity
    {
        APICaller API = new APICaller();
        RecyclerView recycler;
        RVStrikesAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_strikes);
            InitToolbar();
            InitRV();
        }

        private void InitRV()
        {
            List<Strike> strikes = API.GetStrikes(Preferences.Get("idUser", 0));
            recycler = FindViewById<RecyclerView>(Resource.Id.recyclerViewStrikesAc);
            recycler.SetLayoutManager(new LinearLayoutManager(this));
            recycler.SetItemAnimator(new DefaultItemAnimator());
            adapter = new RVStrikesAdapter(strikes);
            recycler.SetAdapter(adapter);
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarStrikes);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Strikes";
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