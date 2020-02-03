﻿using System;
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
        Spinner spinner, spinnerQuantity;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bookATable);
            InitToolbar();
            InitDatePicker();
            InitSpinners();

            TextView politicsButton = FindViewById<TextView>(Resource.Id.politicsText2);
            politicsButton.Click += PopupPolitics;

            Button buttonSearch = FindViewById<Button>(Resource.Id.buttonBooking);
            buttonSearch.Click += PopupConfirmation;
            
        }

        private void PopupConfirmation(object sender, EventArgs e)
        {
            popupDialog = new Dialog(this);
            popupDialog.SetContentView(Resource.Layout.fragment_confirmation);
            popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            popupDialog.Show();

            TextView textDate = popupDialog.FindViewById<TextView>(Resource.Id.confirmationDay);
            TextView textHour = popupDialog.FindViewById<TextView>(Resource.Id.confirmationHour);
            TextView textQuantity = popupDialog.FindViewById<TextView>(Resource.Id.confirmationQuantity);
            Button buttonOk = popupDialog.FindViewById<Button>(Resource.Id.buttonConfirmation);


            textDate.Text = editTextDate.Text;
            textHour.Text = spinner.SelectedItem.ToString();
            textQuantity.Text = spinnerQuantity.SelectedItem.ToString();

            buttonOk.Click += delegate
            {
                popupDialog.Dismiss();
                popupDialog.Hide();
                Intent intent = new Intent(this, typeof(MyReservationsActivity));
                intent.PutExtra(MyReservationsActivity.keyDate, textDate.Text);
                intent.PutExtra(MyReservationsActivity.keyHour, textHour.Text);
                intent.PutExtra(MyReservationsActivity.keyQuantity, textQuantity.Text);
                StartActivity(intent);
            };


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
            spinner = FindViewById<Spinner>(Resource.Id.SpinnerHour);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.AvailableHours, Android.Resource.Layout.SimpleListItem1);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            spinnerQuantity = FindViewById<Spinner>(Resource.Id.SpinnerQuantity);
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