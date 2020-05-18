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
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Util;
using Android.Graphics;
using System.Net;
using System.IO;

namespace MrPiattoClient
{
    [Activity(MainLauncher = true)]
    public class ActivityLogIn : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        GoogleApiClient mGoogleApiClient;
        private ConnectionResult mConnectionResult;
        SignInButton mGsignBtn;
        TextView TxtName, TxtGender;
        ImageView ImgProfile;
        private bool mIntentInProgress;
        private bool mSignInClicked;
        private bool mInfoPopulated;

        APICaller API = new APICaller();
        ImageView facebook, google;
        Button signIn;
        EditText editTextEmail, editTextPassword;

        public string TAG { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_logIn);
            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            InitListeners();
            Preferences.Set("idUser", 3);

            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);
            mGoogleApiClient = builder.Build();
        }

        protected override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }
        protected override void OnStop()
        {
            base.OnStop();
            if (mGoogleApiClient.IsConnected)
            {
                mGoogleApiClient.Disconnect();
            }
        }

        public void OnConnected(Bundle connectionHint)
        {
            var person = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
            var name = string.Empty;
            if (person != null)
            {
                TxtName.Text = person.DisplayName;
                TxtGender.Text = person.Nickname;
                var Img = person.Image.Url;
                var imageBitmap = GetImageBitmapFromUrl(Img.Remove(Img.Length - 5));
                if (imageBitmap != null) ImgProfile.SetImageBitmap(imageBitmap);
            }
        }

        private void ResolveSignInError()
        {
            if (mGoogleApiClient.IsConnecting)
            {
                return;
            }
            if (mConnectionResult.HasResolution)
            {
                try
                {
                    mIntentInProgress = true;
                    StartIntentSenderForResult(mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0);
                }
                catch (Android.Content.IntentSender.SendIntentException io)
                {
                    mIntentInProgress = false;
                    mGoogleApiClient.Connect();
                }
            }
        }
        private Bitmap GetImageBitmapFromUrl(String url)
        {
            Bitmap imageBitmap = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
                return imageBitmap;
            }
            catch (IOException e) { }
            return null;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Log.Debug(TAG, "onActivityResult:" + requestCode + ":" + resultCode + ":" + data);
            if (requestCode == 0)
            {
                if (resultCode != Result.Ok)
                {
                    mSignInClicked = false;
                }
                mIntentInProgress = false;
                if (!mGoogleApiClient.IsConnecting)
                {
                    mGoogleApiClient.Connect();
                }
            }
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                mConnectionResult = result;
                if (mSignInClicked)
                {
                    ResolveSignInError();
                }
            }
        }
        public void OnConnectionSuspended(int cause) { }

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

            google.Click += delegate
            {
                if (!mGoogleApiClient.IsConnecting)
                {
                    mSignInClicked = true;
                    ResolveSignInError();
                }
                else if (mGoogleApiClient.IsConnected)
                {
                    PlusClass.AccountApi.ClearDefaultAccount(mGoogleApiClient);
                    mGoogleApiClient.Disconnect();
                }
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