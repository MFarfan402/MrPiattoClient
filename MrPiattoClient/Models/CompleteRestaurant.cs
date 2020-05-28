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
    public class IdcategoriesNavigation
    {
        public int idcategory { get; set; }
        public string category { get; set; }
        public string urlPhoto { get; set; }
        public IdcategoriesNavigation(int id, string category)
        {
            idcategory = id;
            this.category = category;
            urlPhoto = $"http://200.23.157.109/images/categories/{id}/categorie.jpg";
            //urlPhoto = $"http://192.168.100.207/images/categories/{id}/categorie.jpg";
        }
    }

    public class IdpaymentNavigation
    {
        public int idpaymentOptions { get; set; }
        public string paymentOption { get; set; }
    }

    public class CompleteRestaurant
    {
        public int idrestaurant { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public bool confirmation { get; set; }
        public DateTime lastLogin { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string dress { get; set; }
        public float price { get; set; }
        public float score { get; set; }
        public int severityLevel { get; set; }
        public double @long { get; set; }
        public double lat { get; set; }
        public int idcategories { get; set; }
        public int idpayment { get; set; }
        public string UrlMainFoto { get; set; }
        public IdcategoriesNavigation idcategoriesNavigation { get; set; }
        public IdpaymentNavigation idpaymentNavigation { get; set; }
    }
}