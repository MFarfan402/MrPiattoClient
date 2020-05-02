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
using Xamarin.Essentials;

namespace MrPiattoClient
{
    [Activity(MainLauncher = true)]
    public class ActivityLogIn : Activity
    {
        APICaller API = new APICaller();
        ImageView facebook, google;
        Button signIn;
        EditText editTextEmail, editTextPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            SetContentView(Resource.Layout.activity_logIn);
            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            CheckForPreferences();
            InitListeners();
        }
        /*
        private void CheckForPreferences()
        {
            if(Preferences.ContainsKey("boolUser"))
                if (Preferences.Get("boolUser", false))
                {
                    editTextPassword.Text = 1;
                }
                    
            else
                Preferences.Set("boolUser", false);
        }
        */
        private void InitListeners()
        {
            facebook = FindViewById<ImageView>(Resource.Id.facebookLogIn);
            google = FindViewById<ImageView>(Resource.Id.googleLogIn);
            signIn = FindViewById<Button>(Resource.Id.btnLogIn);
            

            TextView signUp = FindViewById<TextView>(Resource.Id.createAccount);
            signUp.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ActivitySignUp));
                StartActivity(intent);
            };

            TextView forgotPassword = FindViewById<TextView>(Resource.Id.logForgotPassword);
            forgotPassword.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ActivityForgot));
                StartActivity(intent);
            };

            signIn.Click += delegate
            {
                /*
                 * Método temporal xd.
                 
                if(API.LogInUser(editTextEmail.ToString(), editTextPassword.ToString()))
                {
                    
                }
                else
                    Toast.MakeText(this, "Favor de verificar los datos.", ToastLength.Long).Show();
                */
                Intent intent = new Intent(this, typeof(ActivityLoading));
                intent.SetFlags(ActivityFlags.ClearTask);
                StartActivity(intent);
                Finish();

            };
            signIn.LongClick += delegate
            {
                Intent intent = new Intent(this, typeof(ActivityVerifierMain));
                intent.SetFlags(ActivityFlags.ClearTask);
                StartActivity(intent);
                Finish();
            };
            
            

        }
    }
}