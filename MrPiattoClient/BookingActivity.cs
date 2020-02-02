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
        Dialog popupDialog;
        EditText editTextDate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bookATable);
            InitToolbar();
            InitDatePicker();
            InitSpinners();

            TextView politicsButton = FindViewById<TextView>(Resource.Id.politicsText2);
            politicsButton.Click += PopupPolitics;
            
        }

        private void PopupPolitics(object sender, EventArgs e)
        {
            popupDialog = new Dialog(this);
            popupDialog.SetContentView(Resource.Layout.fragment_politics);
            popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);

            popupDialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            popupDialog.Show();

            Button buttonOk = popupDialog.FindViewById<Button>(Resource.Id.buttonOkWarning);
            buttonOk.Click += delegate
            {
                popupDialog.Dismiss();
                popupDialog.Hide();
            };

        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            editTextDate.Text = e.Date.ToShortDateString();
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarBooking);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Haz tu reservación";
        }
        private void InitDatePicker()
        {
            editTextDate = FindViewById<EditText>(Resource.Id.editTextDate);

            editTextDate.Click += delegate
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                dialog.DatePicker.MinDate = today.Millisecond;
                dialog.Show();
            };
        }
        private void InitSpinners()
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.SpinnerHour);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.AvailableHours, Android.Resource.Layout.SimpleListItem1);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            Spinner spinnerQuantity = FindViewById<Spinner>(Resource.Id.SpinnerQuantity);
            var adapterQuantity = ArrayAdapter.CreateFromResource(this, Resource.Array.QuantityGroup, Android.Resource.Layout.SimpleListItem1);
            adapterQuantity.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerQuantity.Adapter = adapterQuantity;
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