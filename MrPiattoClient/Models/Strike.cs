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

namespace MrPiattoClient.Models
{
    public class Strike
    {
        public int idStrike { get; set; }
        public string RestaurantName { get; set; }
        public string StrikeDate { get; set; }
        public string Reason { get; set; }

        public Strike(int idStrike, string restaurantName, string strikeDate, string reason)
        {
            this.idStrike = idStrike;
            RestaurantName = restaurantName;
            StrikeDate = strikeDate;
            Reason = reason;
        }
    }
}