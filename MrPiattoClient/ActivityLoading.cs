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
        int idUser = 3;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_loading);
        }
        protected override void OnResume()
        {
            base.OnResume();
            SimulateStartupAsync();
        }
        private void SimulateStartupAsync()
        {
            NeedToLoadFavorite();
            NeedToLoadReservations();
            Preferences.Set("JSONRes", API.GetMainRestaurantsJSON());
            Preferences.Set("idUser", idUser);
            Intent intent = new Intent(Application.Context, typeof(ActivityHome));
            StartActivity(intent);
        }

        private void NeedToLoadReservations()
        {
            if (Preferences.ContainsKey("boolReservation"))
            {
                if (Preferences.Get("boolReservation", false))
                    return;
                else
                {
                    Preferences.Set("JSONReservation", API.GetReservationsJSON(idUser));
                    Preferences.Set("boolReservation", true);
                }
            }
            else
            {
                Preferences.Set("JSONReservation", API.GetReservationsJSON(idUser));
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
                    Preferences.Set("JSONFavorite", API.GetFavoritesJSON(idUser));
                    Preferences.Set("boolFavorite", true);
                }
            }
            else
            {
                Preferences.Set("JSONFavorite", API.GetFavoritesJSON(idUser));
                Preferences.Set("boolFavorite", true);
            }
        }
        public override void OnBackPressed() { }
    }
}