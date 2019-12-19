using System;
using System.Collections.Generic;


namespace MrPiattoClient.MrPiattoDB
{
    public partial class PaymentOptions
    {
        public int IdpaymentOptions { get; set; }
        public int Idrestaurant { get; set; }
        public string PaymentOption { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}