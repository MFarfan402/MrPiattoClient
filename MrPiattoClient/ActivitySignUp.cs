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
    [Activity(Label = "ActivitySignUp")]
    public class ActivitySignUp : Activity
    {
        EditText password, password2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_signUp);

            InitListeners();
        }

        private void InitListeners()
        {
       
            EditText password = FindViewById<EditText>(Resource.Id.editTextSignPassword);
            EditText password2 = FindViewById<EditText>(Resource.Id.editTextSignPassword2);
            EditText email = FindViewById<EditText>(Resource.Id.editTextSignEmail);
            Button register = FindViewById<Button>(Resource.Id.btnSignUp);
            register.Click += delegate
            {
                if (email.Text.Length == 0 || password.Text.Length == 0 || password2.Text.Length == 0)
                    Toast.MakeText(this, "Favor de llenar todos los campos", ToastLength.Long).Show();
                else
                {
                    if (password.Text.Equals(password2.Text))
                    {
                        Intent intent = new Intent(this, typeof(ActivityPersonalData));
                        intent.PutExtra("email", email.Text);
                        intent.PutExtra("password", password.Text);
                        StartActivity(intent);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "Las contraseñas no coinciden", ToastLength.Long).Show();
                    }
                }
            };
        }
    }
}