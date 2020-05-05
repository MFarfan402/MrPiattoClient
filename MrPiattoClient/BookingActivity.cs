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
using MrPiattoClient.Resources.utilities;
using MrPiattoClient.Models;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace MrPiattoClient
{
    [Activity(Label = "BookingActivity")]
    public class BookingActivity : AppCompatActivity
    {
        APICaller API = new APICaller();
        Policies policies;
        Dialog popupDialog;
        EditText editTextDate;
        Spinner spinner, spinnerQuantity;
        int idRestaurant;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bookATable);
            
            idRestaurant = Intent.GetIntExtra("idRestaurant", 0);
            InitToolbar();
            InitDatePicker();
            InitSpinners();
            CallbackAPI();

            TextView politicsButton = FindViewById<TextView>(Resource.Id.politicsText2);
            politicsButton.Click += PopupPolitics;

            Button buttonSearch = FindViewById<Button>(Resource.Id.buttonBooking);
            buttonSearch.Click += PopupConfirmation;
            
        }

        private void CallbackAPI()
        {
            policies = API.GetPolicies(idRestaurant);
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

            buttonOk.Click += async delegate
            {
                popupDialog.Dismiss();
                popupDialog.Hide();
                DateTime dateTime = Convert.ToDateTime($"{textDate.Text} {textHour.Text}:00");
                Match match = Regex.Match(textQuantity.Text, "([0-9][0-9])");
                
                var response = await API.NewReservation(idRestaurant, Preferences.Get("idUser", 0), dateTime, int.Parse(match.Value));
                Toast.MakeText(this, response, ToastLength.Long).Show();
            };


        }

        private void PopupPolitics(object sender, EventArgs e)
        {
            popupDialog = new Dialog(this);
            popupDialog.SetContentView(Resource.Layout.fragment_politics);
            popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            popupDialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            popupDialog.Show();

            TextView warningA = popupDialog.FindViewById<TextView>(Resource.Id.contentWarningA);
            warningA.Text = $"{policies.maxTimeRes} días";
            TextView warningB = popupDialog.FindViewById<TextView>(Resource.Id.contentWarningB);
            warningB.Text = $"{policies.minTimeRes} horas";
            TextView warningC = popupDialog.FindViewById<TextView>(Resource.Id.contentWarningC);
            warningC.Text = $"{policies.modTimeHours} horas";
            TextView warningE = popupDialog.FindViewById<TextView>(Resource.Id.contentWarningE);
            warningE.Text = (policies.strikes == true) ? "Activada" : "Desactivada";
            TextView warningF = popupDialog.FindViewById<TextView>(Resource.Id.contentWarningF);
            warningF.Text = $"{policies.maxTimeArr} minutos";
            

            Button buttonOk = popupDialog.FindViewById<Button>(Resource.Id.buttonOkWarning);
            buttonOk.Click += delegate
            {
                popupDialog.Dismiss();
                popupDialog.Hide();
            };

        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            editTextDate.Text = e.Date.ToString("yyyy-MM-dd");
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
            editTextDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            
            editTextDate.Click += delegate
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                double minDate = (DateTime.Today + new TimeSpan(1, 0, 0, 0) - new DateTime(1970, 1, 1)).TotalMilliseconds;
                double maxDate = (new DateTime(today.Year, today.Month, today.Day) - new DateTime(1970, 1, 1) + new TimeSpan(7,0,0,0))
                .TotalMilliseconds;
                dialog.DatePicker.MaxDate = (long)maxDate;
                dialog.DatePicker.MinDate = (long)minDate;
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