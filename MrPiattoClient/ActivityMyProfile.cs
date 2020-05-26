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
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityMyProfile")]
    public class ActivityMyProfile : AppCompatActivity
    {
        Spinner spinnerGender;
        EditText name, lastName, mail, phone;
        User user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_personalData);
            InitToolbar();
            InitSpinners();
            InitListeners();
            PutInfo();

        }

        private void PutInfo()
        {
            user = JsonConvert.DeserializeObject<User>(Preferences.Get("userInfo", null));

            name = FindViewById<EditText>(Resource.Id.editInputName);
            name.Text = user.FirstName;

            lastName = FindViewById<EditText>(Resource.Id.editInputLastName);
            lastName.Text = user.LastName;

            mail = FindViewById<EditText>(Resource.Id.editInputEmail);
            mail.Text = user.Mail;

            phone = FindViewById<EditText>(Resource.Id.editInputPhone);
            phone.Text = user.Phone;
        }

        private void InitListeners()
        {
            Button save = FindViewById<Button>(Resource.Id.btnSavePersonalData);
            save.Click += delegate
            {
                Finish();
            };
        }

        private void InitSpinners()
        {
            spinnerGender = FindViewById<Spinner>(Resource.Id.spinnerGender2);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Gender, Android.Resource.Layout.SimpleListItem1);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerGender.Adapter = adapter;
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarMyData);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Mi perfil";
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