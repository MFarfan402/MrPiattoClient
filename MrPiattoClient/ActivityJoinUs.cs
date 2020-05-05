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
using MrPiattoClient.Resources.utilities;
using System.Threading.Tasks;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityJoinUs")]
    public class ActivityJoinUs : AppCompatActivity
    {
        APICaller API = new APICaller();
        EditText name, address, phone, mail;
        Button button;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_workWithUs);
            InitToolbar();
            InitListeners();
        }

        private void InitListeners()
        {
            name = FindViewById<EditText>(Resource.Id.editJoinName);
            address = FindViewById<EditText>(Resource.Id.editJoinAddress);
            phone = FindViewById<EditText>(Resource.Id.editJoinPhone);
            mail = FindViewById<EditText>(Resource.Id.editJoinMail);
            button = FindViewById<Button>(Resource.Id.btnNewRestaurant);
            button.Click += async delegate { await SendRequestAsync(); };
        }

        private async Task SendRequestAsync()
        {
            if (name.Text.Length == 0 || address.Text.Length == 0 || phone.Text.Length == 0 || mail.Text.Length == 0)
                Toast.MakeText(this, "Favor de llenar todos los campos", ToastLength.Long).Show();
            else
            {
                var msg = await API.JoinMrPiatto(name.Text, address.Text, phone.Text, mail.Text);
                Toast.MakeText(this, msg, ToastLength.Long).Show();
            }
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarWorkWithUs);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Únete a nuestro equipo";
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