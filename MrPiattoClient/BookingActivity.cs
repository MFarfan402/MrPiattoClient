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
    [Activity(Label = "BookingActivity")]
    public class BookingActivity : AppCompatActivity
    {
        EditText editTextQuantity, editTextDate, editTextHour;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bookATable);
            InitToolbar();

            editTextQuantity = FindViewById<EditText>(Resource.Id.editTextQuantity);
            editTextDate = FindViewById<EditText>(Resource.Id.editTextDate);
            editTextHour = FindViewById<EditText>(Resource.Id.editTextHour);

            editTextDate.Click += delegate
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                dialog.DatePicker.MinDate = today.Millisecond;
                dialog.Show();
            };
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            editTextDate.Text = e.Date.ToLongDateString();
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarBooking);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Haz tu reservación";
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