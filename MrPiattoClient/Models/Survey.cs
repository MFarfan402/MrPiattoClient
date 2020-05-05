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
    public class IdcommentNavigation
    {
        public int idcomment { get; set; }
        public int idrestaurant { get; set; }
        public int iduser { get; set; }
        public string comment { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public object idrestaurantNavigation { get; set; }
        public object iduserNavigation { get; set; }
        public List<Survey> surveys { get; set; }
    }

    public class Survey
    {
        public int idsurvey { get; set; }
        public int idrestaurant { get; set; }
        public int iduser { get; set; }
        public int idwaiter { get; set; }
        public double foodRating { get; set; }
        public double comfortRating { get; set; }
        public double serviceRating { get; set; }
        public double generalScore { get; set; }
        public int idcomment { get; set; }
        public DateTime dateStatistics { get; set; }
        public IdcommentNavigation idcommentNavigation { get; set; }
        public object idrestaurantNavigation { get; set; }
        public object iduserNavigation { get; set; }
        public object idwaiterNavigation { get; set; }
    }
}