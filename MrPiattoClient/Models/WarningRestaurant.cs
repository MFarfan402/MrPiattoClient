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
    public class WarningRestaurant
    {
        public int idrestaurant { get; set; }
        public DateTime lastLogin { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string UrlMainFoto { get; set; }

        public WarningRestaurant(int idrestaurant, DateTime lastLogin, string name, string phone)
        {
            this.idrestaurant = idrestaurant;
            this.lastLogin = lastLogin;
            this.name = name;
            this.phone = phone;
            //UrlMainFoto = $"http://192.168.100.207/images/{idrestaurant}/main.jpg";
            UrlMainFoto = $"http://200.23.157.109/images/{idrestaurant}/main.jpg";
        }
    }
}