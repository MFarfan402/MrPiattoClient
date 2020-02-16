using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;

namespace MrPiattoClient
{
    [Activity(Label = "MyReservationsActivity")]
    public class MyReservationsActivity : AppCompatActivity
    {
        public static readonly string keyDate = "keyDate", keyHour = "keyHour", keyQuantity = "keyQuantity";
        RecyclerView recycler;
        RecyclerViewReservationAdapter adapter;
        List<Reservation> reservations = new List<Reservation>()
        {
            new Reservation("03/02/2020", "13:00", "3 personas", "Mr. Piatto Restaurant"),
            new Reservation("05/02/2020", "19:00", "6 personas", "Fonda de Doña Chona")
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_myReservations);
            InitToolbar();
            reservations.Add(new Reservation(
                Intent.GetStringExtra(keyDate),
                Intent.GetStringExtra(keyHour),
                Intent.GetStringExtra(keyQuantity),
                "Mr. Piatto Restaurant"
                ));
            ReservationsOnScreen();
        }

        private void ReservationsOnScreen()
        {
            recycler = FindViewById<RecyclerView>(Resource.Id.recyclerViewMyReservations);
            recycler.SetLayoutManager(new LinearLayoutManager(this));
            recycler.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewReservationAdapter(reservations, this);
            recycler.SetAdapter(adapter);
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarMyReservations);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Mis reservaciones";
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