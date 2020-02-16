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
using AndroidX.RecyclerView.Widget;

namespace MrPiattoClient.Resources.adapter
{
    class Reservation
    {
        public string Date { get; set; }
        public string Hour { get; set; }
        public string Quantity { get; set; }
        public string RestaurantName { get; set; }
        //public string ImageString { get; set; }
        public Reservation(string Date, string Hour, string Quantity, string RestaurantName)
        {
            this.Date = Date;
            this.Hour = Hour;
            this.Quantity = Quantity;
            this.RestaurantName = RestaurantName;
        }
    }


    class RecyclerViewReservationHolder : RecyclerView.ViewHolder
    {
        public TextView date, hour, quantity, restaurantName;
        public Button buttonModify;
        //public ImageView image;
        public RecyclerViewReservationHolder(View itemView) : base(itemView)
        {
            date = itemView.FindViewById<TextView>(Resource.Id.cardviewDay);
            hour = itemView.FindViewById<TextView>(Resource.Id.cardviewHour);
            quantity = itemView.FindViewById<TextView>(Resource.Id.cardviewQuantity);
            restaurantName = itemView.FindViewById<TextView>(Resource.Id.cardviewReservationsName);
            //image = itemView.FindViewById<ImageView>(Resource.Id.cardviewRestaurantImage);
            buttonModify = itemView.FindViewById<Button>(Resource.Id.buttonModifyMyReservation);
        }
    }

    class RecyclerViewReservationAdapter : RecyclerView.Adapter
    {
        private Context context;
        public List<Reservation> reservations;

        public RecyclerViewReservationAdapter(List<Reservation> reservations, Context context)
        {
            this.reservations = reservations;
            this.context = context;
        }

        public override int ItemCount
        {
            get { return reservations.Count(); }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewReservationHolder viewHolder = holder as RecyclerViewReservationHolder;
            viewHolder.date.Text = reservations[position].Date;
            viewHolder.hour.Text = reservations[position].Hour;
            viewHolder.quantity.Text = reservations[position].Quantity;
            viewHolder.restaurantName.Text = reservations[position].RestaurantName;
            //viewHolder.image.SetImageResource(reservations.R);
            viewHolder.buttonModify.Click += (sender, e) =>
            {
                Intent intent = new Intent(context, typeof(ModifyActivity));
                context.StartActivity(intent);
            };

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_myReservation, parent, false);
            return new RecyclerViewReservationHolder(itemView);
        }
    }

}