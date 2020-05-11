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
        public int idcomment { get; set; }
        public double rating { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string comment { get; set; }
        public Comment(int idcomment, double rating, string name, string date, string comment)
        {
            this.idcomment = idcomment;
            this.rating = rating;
            this.name = name;
            this.date = date;
            this.comment = comment;
        }
    }

}