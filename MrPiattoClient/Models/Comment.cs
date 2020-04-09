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
    public class Comment
    {
        public int rating { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string comment { get; set; }
        public Comment(int rating, string name, string date, string comment)
        {
            this.rating = rating;
            this.name = name;
            this.date = date;
            this.comment = comment;
        }
    }

}