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
    public class Policies
    {
        public int idpolicies { get; set; }
        public int idrestaurant { get; set; }
        public int maxTimeRes { get; set; }
        public int minTimeRes { get; set; }
        public int modTime { get; set; }
        public bool strikes { get; set; }
        public int strikeType { get; set; }
        public int maxTimeArr { get; set; }
        public object idrestaurantNavigation { get; set; }
    }
}