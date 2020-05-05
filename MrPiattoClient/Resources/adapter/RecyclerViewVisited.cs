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
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient.Resources.adapter
{
    class RecyclerViewVisitedHolder : RecyclerView.ViewHolder
    {
        public TextView name, location, cuisine;
        public RatingBar ratingBar;
        public Button buttonRateMe, buttonComplaint;
        public ImageView image;
        public RecyclerViewVisitedHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewVisitedRestaurantName);
            image = itemView.FindViewById<ImageView>(Resource.Id.cardviewVisitedImage);
            location = itemView.FindViewById<TextView>(Resource.Id.cardviewVisitedLocation);
            cuisine = itemView.FindViewById<TextView>(Resource.Id.cardviewVisitedRestaurantCuisine);
            ratingBar = itemView.FindViewById<RatingBar>(Resource.Id.cardviewVisitedRatingBar);
            buttonRateMe = itemView.FindViewById<Button>(Resource.Id.buttonVisitedReview);
            buttonComplaint = itemView.FindViewById<Button>(Resource.Id.buttonVisitedComplaint);
        }
    }

    class RecyclerViewVisitedAdapter : RecyclerView.Adapter
    {
        private Context context;
        private List<Restaurant> visitedRestaurant;
        public RecyclerViewVisitedAdapter(List<Restaurant> visitedRestaurant, Context context)
        {
            this.visitedRestaurant = visitedRestaurant;
            this.context = context;
        }

        public override int ItemCount
        {
            get { return visitedRestaurant.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewVisitedHolder viewHolder = holder as RecyclerViewVisitedHolder;
            viewHolder.name.Text = visitedRestaurant[position].Name;
            viewHolder.location.Text = visitedRestaurant[position].Address;
            viewHolder.cuisine.Text = visitedRestaurant[position].Categories;
            viewHolder.ratingBar.Rating = (float)visitedRestaurant[position].Rating;
            viewHolder.image.SetImageBitmap(ImageHelper.GetImageBitmapFromUrl(visitedRestaurant[position].UrlMainFoto));
            viewHolder.buttonRateMe.Click += (sender, e) =>
            {
                Intent intent = new Intent(context, typeof(SurveyActivity));
                context.StartActivity(intent);
            };
            viewHolder.buttonComplaint.Click += (sender, e) =>
            {
                Intent intent = new Intent(context, typeof(ComplaintActivity));
                context.StartActivity(intent);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_visited, parent, false);
            return new RecyclerViewVisitedHolder(itemView);
        }
    }
}