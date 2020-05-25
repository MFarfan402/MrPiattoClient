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
using Xamarin.Essentials;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityNotification")]
    public class ActivityNotification : AppCompatActivity
    {
        APICaller API = new APICaller();
        Switch push, mail;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_notifications);
            InitToolbar();
            InitListener();
        }

        private void InitListener()
        {
            Button save = FindViewById<Button>(Resource.Id.buttonNotificationSave);
            push = FindViewById<Switch>(Resource.Id.allowPushNew);
            mail = FindViewById<Switch>(Resource.Id.allowMailNew);

            push.Checked = Preferences.Get("boolNFPush", true);
            mail.Checked = Preferences.Get("boolNFMail", true);

            save.Click += delegate
            {
                if(!mail.Checked)
                {
                    string msg = API.DisableMail(Preferences.Get("idUser", 0)) ? "Ha sido eliminadas las suscripciones del " +
                    "boletín de los restaurantes." : "De ahora en adelante, recibirá notificaciones en su correo";
                    Toast.MakeText(this, msg, ToastLength.Long).Show();
                }
                Preferences.Set("boolNFPush", push.Checked);
                Preferences.Set("boolNFMail", mail.Checked);
                Finish();
            };
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarNotifications);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Notificaciones";
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