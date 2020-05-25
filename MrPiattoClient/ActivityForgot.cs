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
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityForgot")]
    public class ActivityForgot : Activity
    {
        APICaller API = new APICaller();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_forgotPassword);
            InitListeners();
        }

        private void InitListeners()
        {
            EditText emailText = FindViewById<EditText>(Resource.Id.editTextEmailForgot);
            Button email = FindViewById<Button>(Resource.Id.btnEmailForgot);
            email.Click += delegate
            {
                if (emailText.Text.Length == 0)
                    Toast.MakeText(this, "Favor de llenar el campo.", ToastLength.Long).Show();
                else
                {
                    string msg = API.NewPassword(emailText.Text);
                    Toast.MakeText(this, msg, ToastLength.Long).Show();
                    Finish();
                }
            };
        }
    }
}