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

namespace MrPiattoClient.Models
{
    public class Restaurant
    {
        public int IDRestaurant { get; set; }
        public double Rating { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Categories { get; set; }
        public string UrlMainFoto { get; set; }

        public Restaurant(int id, double rating, string name, string address, string categorie)
        {
            IDRestaurant = id;
            Rating = rating;
            Name = name;
            Address = address;
            Categories = categorie;
            UrlMainFoto = $"http://192.168.100.207/images/{IDRestaurant}/main.jpg";
        }
    }
}