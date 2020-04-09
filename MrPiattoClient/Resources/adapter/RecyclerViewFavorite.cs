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
    class RecyclerViewFavoriteHolder : RecyclerView.ViewHolder
    {
        public TextView name, location, cuisine;
        public RatingBar ratingBar;
        public RecyclerViewFavoriteHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteRestaurantName);
            location = itemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteLocation);
            cuisine = itemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteRestaurantCuisine);
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
            viewHolder.name.Text = favoriteRestaurant[position].Name;
            viewHolder.location.Text = favoriteRestaurant[position].Address;
            viewHolder.cuisine.Text = favoriteRestaurant[position].Categories;
            viewHolder.ratingBar.Rating = favoriteRestaurant[position].Rating;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_favorite, parent, false);
            return new RecyclerViewFavoriteHolder(itemView);
        }
    }
}