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

namespace MrPiattoClient
{
    [Activity(Label = "ActivityForgot")]
    public class ActivityForgot : Activity
    {
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
                    Toast.MakeText(this, "Se ha enviado un correo para restablecer la contraseña.", ToastLength.Long).Show();
                    Finish();
                }
            };
            
            
        }
    }
}