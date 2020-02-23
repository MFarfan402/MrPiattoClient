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
    class RecyclerViewMainHolder : RecyclerView.ViewHolder
    {
        public TextView name, cuisine, rating;
        public RecyclerViewMainHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewMainName);
            cuisine = itemView.FindViewById<TextView>(Resource.Id.cardviewMainCuisine);
            rating = itemView.FindViewById<TextView>(Resource.Id.cardviewMainRating);
        }
    }

    class RecyclerViewMainAdapter : RecyclerView.Adapter
    {
        private List<Restaurant> restaurants;
        public RecyclerViewMainAdapter(List<Restaurant> restaurants)
        {
            this.restaurants = restaurants;
        }
        public override int ItemCount
        {
            get { return restaurants.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewMainHolder viewHolder = holder as RecyclerViewMainHolder;
            viewHolder.name.Text = restaurants[position].name;
            viewHolder.cuisine.Text = restaurants[position].cuisine;
            viewHolder.rating.Text = restaurants[position].rating.ToString(); ;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_restaurantHome, parent, false);
            return new RecyclerViewMainHolder(itemView);
        }
    }
}