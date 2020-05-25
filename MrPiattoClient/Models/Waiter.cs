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
    public class Waiter
    {
        public int idwaiter { get; set; }
        public int idrestaurant { get; set; }
        public string waiterFirstName { get; set; }
        public string waiterLasName { get; set; }

        public override string ToString()
        {
            return $"{idwaiter}. {waiterFirstName} {waiterLasName}";
        }
    }
}