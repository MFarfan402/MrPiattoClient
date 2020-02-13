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
    class RecyclerViewVisitedHolder : RecyclerView.ViewHolder
    {
        public TextView name, location, cuisine;
        public RatingBar ratingBar;
        //public Button buttonRateMe, buttonComplaint;
        public RecyclerViewVisitedHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewVisitedRestaurantName);
            location = itemView.FindViewById<TextView>(Resource.Id.cardviewVisitedLocation);
            cuisine = ItemView.FindViewById<TextView>(Resource.Id.cardviewVisitedRestaurantCuisine);
            ratingBar = itemView.FindViewById<RatingBar>(Resource.Id.cardviewVisitedRatingBar);
            //buttonRateMe = itemView.FindViewById<Button>(Resource.Id.buttonVisitedReview);
            //buttonComplaint = itemView.FindViewById<Button>(Resource.Id.buttonVisitedComplaint);
        }
    }

    class RecyclerViewVisitedAdapter : RecyclerView.Adapter
    {
        private List<Restaurant> visitedRestaurant;
        public RecyclerViewVisitedAdapter(List<Restaurant> visitedRestaurant)
        {
            this.visitedRestaurant = visitedRestaurant;
        }

        public override int ItemCount
        {
            get { return visitedRestaurant.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewVisitedHolder viewHolder = holder as RecyclerViewVisitedHolder;
            viewHolder.name.Text = visitedRestaurant[position].name;
            viewHolder.location.Text = visitedRestaurant[position].location;
            viewHolder.cuisine.Text = visitedRestaurant[position].cuisine;
            viewHolder.ratingBar.Rating = visitedRestaurant[position].rating;
            //viewHolder.buttonRateMe.Click += StartActivityReview();
            //viewHolder.buttonComplaint.Click += StartActivityComplaint();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_visited, parent, false);
            return new RecyclerViewVisitedHolder(itemView);
        }
        /*
        private EventHandler StartActivityReview()
        {
            throw new NotImplementedException();
        }

        private EventHandler StartActivityComplaint()
        {
            throw new NotImplementedException();
        }*/
    }
}