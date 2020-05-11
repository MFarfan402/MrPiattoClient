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
using MrPiattoClient.Models;
using Newtonsoft.Json;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityAddRestaurant")]
    public class ActivityAddRestaurant : AppCompatActivity
    {
        NewRestaurant newRestaurant;
        string password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_acceptRestaurant);
            InitToolbar();
            GetDataIntent();
            InflateData();
        }

        private void InflateData()
        {
            TextView mail = FindViewById<TextView>(Resource.Id.infoMail);
            TextView infoPassword = FindViewById<TextView>(Resource.Id.infoPassword);
            Button buttonOk = FindViewById<Button>(Resource.Id.btnSendRestaurantData);
            buttonOk.Click += delegate
            {
                Finish();
            };

            mail.Text = newRestaurant.mail;
            infoPassword.Text = password;
        }

        private void GetDataIntent()
        {
            newRestaurant = JsonConvert.DeserializeObject<NewRestaurant>(Intent.GetStringExtra("JSONRes"));
            password = Intent.GetStringExtra("password");
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarAcceptRestaurant);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Bienvenido a Mr. Piatto";
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