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
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MrPiattoClient
{
    [Activity(Label = "ModifyActivity")]
    public class ModifyActivity : AppCompatActivity
    {
        Dialog popupDialog;
        EditText editTextDate;
        Spinner spinner, spinnerQuantity;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_modifyReservation);
            InitToolbar();
            InitDatePicker();
            InitSpinners();


            TextView politicsButton = FindViewById<TextView>(Resource.Id.politicsTextModify2);
            //politicsButton.Click += PopupPolitics;

            Button buttonSearch = FindViewById<Button>(Resource.Id.buttonModify);
            //buttonSearch.Click += PopupConfirmation;
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            editTextDate.Text = e.Date.ToShortDateString();
        }
        private void InitDatePicker()
        {
            editTextDate = FindViewById<EditText>(Resource.Id.editTextDateModify);

            editTextDate.Click += delegate
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                dialog.DatePicker.MinDate = today.Millisecond;
                dialog.Show();
            };
        }
        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarModify);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Modifica tu reservación";
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
        private void InitSpinners()
        {
            spinner = FindViewById<Spinner>(Resource.Id.SpinnerHourModify);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.AvailableHours, Android.Resource.Layout.SimpleListItem1);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            spinnerQuantity = FindViewById<Spinner>(Resource.Id.SpinnerQuantityModify);
            var adapterQuantity = ArrayAdapter.CreateFromResource(this, Resource.Array.QuantityGroup, Android.Resource.Layout.SimpleListItem1);
            adapterQuantity.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerQuantity.Adapter = adapterQuantity;
        }
    }
}