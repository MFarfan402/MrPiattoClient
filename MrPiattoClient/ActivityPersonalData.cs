﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MrPiattoClient.Models;
using MrPiattoClient.Resources.utilities;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityPersonalData")]
    public class ActivityPersonalData : Activity
    {
        APICaller API = new APICaller();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_personalData);

            InitListeners();
        }

        private void InitListeners()
        {
            EditText name = FindViewById<EditText>(Resource.Id.editTextPersonalName);
            EditText lastName = FindViewById<EditText>(Resource.Id.editTextPersonalLastName);
            EditText phone = FindViewById<EditText>(Resource.Id.editTextPersonalPhone);
            Button endSignUp = FindViewById<Button>(Resource.Id.btnEndSign);
            endSignUp.Click += async delegate
            {
                if (name.Text.Length == 0|| lastName.Text.Length == 0 || phone.Text.Length == 0)
                    Toast.MakeText(this, "Favor de llenar todos los campos", ToastLength.Long).Show();
                else
                {
                    User user = new User();
                    user.FirstName = name.Text;
                    user.LastName = lastName.Text;
                    user.Phone = phone.Text;
                    user.Mail = Intent.GetStringExtra("email");
                    user.Password = Intent.GetStringExtra("password");
                    user.UserType = "user";

                    Preferences.Set("idUser", await API.CreateUserAsync(user));
                    Preferences.Set("boolUser", true);
                    Preferences.Set("userName", user.FirstName);
                    Preferences.Set("userInfo", JsonConvert.SerializeObject(user));
                    Preferences.Set("boolReservation", false);
                    Preferences.Set("boolFavorite", false);

                    Intent intent = new Intent(this, typeof(ActivityLoading));
                    StartActivity(intent);
                    Finish();
                }                
            };
        }
    }
}