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
    
    public class Reservation
    {
        public int idreservation { get; set; }
        public int iduser { get; set; }
        public int idtable { get; set; }
        public DateTime date { get; set; }
        public int amountOfPeople { get; set; }
        public string url { get; set; }
        public IdtableNavigation idtableNavigation { get; set; }
    }

    public class IdtableNavigation
    {
        public int idtables { get; set; }
        public int idrestaurant { get; set; }
        public IdrestaurantNavigation idrestaurantNavigation { get; set; }
    }

}