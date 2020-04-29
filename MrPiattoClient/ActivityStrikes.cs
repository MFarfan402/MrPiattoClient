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

namespace MrPiattoClient
{
    [Activity(Label = "ActivityStrikes")]
    public class ActivityStrikes : AppCompatActivity
    {
        List<Strike> strikes = new List<Strike>()
        {
            new Strike(1, "MrPiattoRestaurant", "2020-04-20", "No asistió a la reservación"),
            new Strike(2, "MrPiattoRestaurant", "2020-04-20", "No le gusto la comida")
        };
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