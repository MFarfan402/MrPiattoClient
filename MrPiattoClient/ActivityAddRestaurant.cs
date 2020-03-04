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

namespace MrPiattoClient
{
    [Activity(Label = "ActivityAddRestaurant")]
    public class ActivityAddRestaurant : AppCompatActivity
    {
        EditText initHour, finalHour;
        Spinner categorie1, categorie2, categorie3, dressSpinner;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_acceptRestaurant);
            InitToolbar();
            InitSpinners();
            InitTimePickers();
        }

        private void InitTimePickers()
        {
            initHour = FindViewById<EditText>(Resource.Id.editTextInitHour);
            finalHour = FindViewById<EditText>(Resource.Id.editTextFinalHour);

            initHour.Click += delegate
            {
                DateTime currentTime = DateTime.Now;
                TimePickerDialog dialog = new TimePickerDialog(this, OnTimeSetInit, currentTime.Hour, currentTime.Minute, true);
                dialog.Show();
            };
            finalHour.Click += delegate
            {
                DateTime currentTime = DateTime.Now;
                TimePickerDialog dialog = new TimePickerDialog(this, OnTimeSetFinal, currentTime.Hour, currentTime.Minute, true);
                dialog.Show();
            };
        }

        private void OnTimeSetInit(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            initHour.Text = e.HourOfDay.ToString() + " : " + e.Minute.ToString();
        }

        private void OnTimeSetFinal(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            finalHour.Text = e.HourOfDay.ToString() + " : " + e.Minute.ToString();
        }


        private void InitSpinners()
        {
            categorie1 = FindViewById<Spinner>(Resource.Id.SpinnerCategorieFirst);
            categorie2 = FindViewById<Spinner>(Resource.Id.SpinnerCategorieSecond);
            categorie3 = FindViewById<Spinner>(Resource.Id.SpinnerCategorieThird);
            dressSpinner = FindViewById<Spinner>(Resource.Id.SpinnerDressAccept);

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Cuisines, Android.Resource.Layout.SimpleListItem1);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            categorie1.Adapter = adapter;
            categorie2.Adapter = adapter;
            categorie3.Adapter = adapter;

            var adapterDress = ArrayAdapter.CreateFromResource(this, Resource.Array.Dress, Android.Resource.Layout.SimpleListItem1);
            adapterDress.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            dressSpinner.Adapter = adapterDress;
        }
        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarAcceptRestaurant);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Agrega tus datos";
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