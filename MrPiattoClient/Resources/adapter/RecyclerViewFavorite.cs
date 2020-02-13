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
    class RecyclerViewFavoriteHolder : RecyclerView.ViewHolder
    {
        public TextView name, location, cuisine;
        public RatingBar ratingBar;
        public RecyclerViewFavoriteHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteRestaurantName);
            location = itemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteLocation);
            cuisine = ItemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteRestaurantCuisine);
            ratingBar = itemView.FindViewById<RatingBar>(Resource.Id.cardviewFavoriteRatingBar);
        }
    }
    class RecyclerViewFavoriteAdapter : RecyclerView.Adapter
    {

        private List<Restaurant> favoriteRestaurant;
        public RecyclerViewFavoriteAdapter(List<Restaurant> favoriteRestaurant)
        {
            this.favoriteRestaurant = favoriteRestaurant;
        }

        public override int ItemCount
        {
            get { return favoriteRestaurant.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewFavoriteHolder viewHolder = holder as RecyclerViewFavoriteHolder;
            viewHolder.name.Text = favoriteRestaurant[position].name;
            viewHolder.location.Text = favoriteRestaurant[position].location;
            viewHolder.cuisine.Text = favoriteRestaurant[position].cuisine;
            viewHolder.ratingBar.Rating = favoriteRestaurant[position].rating;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_favorite, parent, false);
            return new RecyclerViewFavoriteHolder(itemView);
        }
    }

    public class Restaurant
    {
        public int rating { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string cuisine { get; set; }
        public Restaurant(int rating, string name, string location, string cuisine)
        {
            this.rating = rating;
            this.name = name;
            this.location = location;
            this.cuisine = cuisine;
        }
    }
}