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
using MrPiattoClient.Models;


namespace MrPiattoClient.Resources.adapter
{
    class RecyclerViewMainHolder : RecyclerView.ViewHolder
    {
        public TextView name, cuisine, rating;
        public Button reservation;
        public RecyclerViewMainHolder(View itemView) : base(itemView)
        {
            reservation = itemView.FindViewById<Button>(Resource.Id.btnBookATableMain);
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewMainName);
            cuisine = itemView.FindViewById<TextView>(Resource.Id.cardviewMainCuisine);
            rating = itemView.FindViewById<TextView>(Resource.Id.cardviewMainRating);
        }
    }

    class RecyclerViewMainAdapter : RecyclerView.Adapter
    {
        private List<Restaurant> restaurants;
        private Context context;
        public RecyclerViewMainAdapter(List<Restaurant> restaurants, Context context)
        {
            this.restaurants = restaurants;
            this.context = context;
        }
        public override int ItemCount
        {
            get { return restaurants.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewMainHolder viewHolder = holder as RecyclerViewMainHolder;
            viewHolder.name.Text = restaurants[position].Name;
            viewHolder.cuisine.Text = restaurants[position].Categories;
            viewHolder.rating.Text = restaurants[position].Rating.ToString();
            viewHolder.reservation.Click += (sender, e) =>
            {
                Intent intent = new Intent(context, typeof(RestaurantActivity));
                context.StartActivity(intent);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_restaurantHome, parent, false);
            return new RecyclerViewMainHolder(itemView);
        }
    }
}