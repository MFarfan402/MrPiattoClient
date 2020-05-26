using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityLoading", NoHistory = true)]
    public class ActivityLoading : AppCompatActivity
    {
        APICaller API = new APICaller();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SimulateStartupAsync();
        }
        protected override void OnResume()
        {
            base.OnResume();
        }
        private void SimulateStartupAsync()
        {
            NeedToLoadFavorite();
            NeedToLoadReservations();
            NotificationsPreferences();
            Preferences.Set("JSONRes", API.GetMainRestaurantsJSON());
            Intent intent = new Intent(Application.Context, typeof(ActivityHome));
            StartActivity(intent);
        }

        private void NotificationsPreferences()
        {
            if (!Preferences.ContainsKey("boolNFPush"))
            {
                Preferences.Set("boolNFPush", true);
                Preferences.Set("boolNFMail", true);
            }
        }

        private void NeedToLoadReservations()
        {
            if (Preferences.ContainsKey("boolReservation"))
            {
                if (Preferences.Get("boolReservation", false))
                    return;
                else
                {
                    Preferences.Set("JSONReservation", API.GetReservationsJSON(Preferences.Get("idUser", 0)));
                    Preferences.Set("boolReservation", true);
                }
            }
            else
            {
                Preferences.Set("JSONReservation", API.GetReservationsJSON(Preferences.Get("idUser", 0)));
                Preferences.Set("boolReservation", true);
            }
        }

        private void NeedToLoadFavorite()
        {
            if (Preferences.ContainsKey("boolFavorite"))
            {
                if (Preferences.Get("boolFavorite", false))
                    return;
                else
                {
                    Preferences.Set("JSONFavorite", API.GetFavoritesJSON(Preferences.Get("idUser", 0)));
                    Preferences.Set("boolFavorite", true);
                }
            }
            else
            {
                Preferences.Set("JSONFavorite", API.GetFavoritesJSON(Preferences.Get("idUser", 0)));
                Preferences.Set("boolFavorite", true);
            }
        }
        public override void OnBackPressed() { }
    }
}