using System;
using System.Collections.Generic;

namespace MrPiattoClient.MrPiattoDB
{
    public partial class UserPhotos
    {
        public int IduserPhotos { get; set; }
        public int Idrestaurant { get; set; }
        public int Iduser { get; set; }
        public string Url { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
    }
}