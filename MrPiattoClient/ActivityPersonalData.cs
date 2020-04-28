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
    [Activity(Label = "ActivityPersonalData")]
    public class ActivityPersonalData : Activity
    {
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
            endSignUp.Click += delegate
            {
                if (name.Text.Length == 0|| lastName.Text.Length == 0 || phone.Text.Length == 0)
                    Toast.MakeText(this, "Favor de llenar todos los campos", ToastLength.Long).Show();
                else
                {
                    string email = Intent.GetStringExtra("email");
                    string password = Intent.GetStringExtra("password");
                    Intent intent = new Intent(this, typeof(ActivityHome));
                    StartActivity(intent);
                    Finish();
                }
                // Llamada a la base de datos para insertar usuario.
                
            };
        }
    }
}