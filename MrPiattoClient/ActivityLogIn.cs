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
    [Activity(MainLauncher = true)]
    public class ActivityLogIn : Activity
    {
        ImageView facebook, google;
        Button signIn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_logIn);
            InitListeners();
        }

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


            signIn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ActivityHome));
                StartActivity(intent);
                Finish();
            };
            signIn.LongClick += delegate
            {
                Intent intent = new Intent(this, typeof(ActivityVerifierMain));
                StartActivity(intent);
                Finish();
            };
            
            

        }
    }
}