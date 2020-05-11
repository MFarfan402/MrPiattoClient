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
using MrPiattoClient.Models;
using Newtonsoft.Json;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    [Activity(Label = "ModifyActivity")]
    public class ModifyActivity : AppCompatActivity
    {
        Dialog popupDialog;
        EditText editTextDate;
        Spinner spinner, spinnerQuantity;
        Reservation reservation;
        Policies policies;
        APICaller API = new APICaller();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_modifyReservation);
            reservation = JsonConvert.DeserializeObject<Reservation>(Intent.GetStringExtra("JSONReservation"));
            policies = API.GetPolicies(reservation.idtableNavigation.idrestaurant);
            InitToolbar();
            InitDatePicker();
            InitSpinners();


            TextView politicsButton = FindViewById<TextView>(Resource.Id.politicsTextModify2);
            politicsButton.Click += PopupPolitics;

            Button buttonSearch = FindViewById<Button>(Resource.Id.buttonModify);
            //buttonSearch.Click += PopupConfirmation;
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
        private void InitDatePicker()
        {
            editTextDate = FindViewById<EditText>(Resource.Id.editTextDateModify);
            editTextDate.Text = reservation.date.ToString("yyyy-MM-dd");
            editTextDate.Click += delegate
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                double minDate = (DateTime.Today + new TimeSpan(1, 0, 0, 0) - new DateTime(1970, 1, 1)).TotalMilliseconds;
                double maxDate = (new DateTime(today.Year, today.Month, today.Day) - new DateTime(1970, 1, 1) + new TimeSpan(7, 0, 0, 0))
                .TotalMilliseconds;
                dialog.DatePicker.MaxDate = (long)maxDate;
                dialog.DatePicker.MinDate = (long)minDate;
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

            spinnerQuantity.SetSelection(reservation.amountOfPeople - 1);
            spinner.SetSelection(reservation.date.Hour - 1);
        }
    }
}