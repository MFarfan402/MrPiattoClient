﻿using System;
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
    public class Strike
    {
        public int iduserStrikes { get; set; }
        public int iduser { get; set; }
        public DateTime date { get; set; }
        public string reason { get; set; }
    }
}