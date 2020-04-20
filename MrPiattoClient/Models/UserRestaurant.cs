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
    public class IdrestaurantNavigation
    {
        public int idrestaurant { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public double score { get; set; }
    }

    public class UserRestaurant
    {
        public int iduserRestaurant { get; set; }
        public int idrestaurant { get; set; }
        public int iduser { get; set; }
        public bool visited { get; set; }
        public bool mailSubscription { get; set; }
        public bool favorite { get; set; }
        public IdrestaurantNavigation idrestaurantNavigation { get; set; }
    }
}