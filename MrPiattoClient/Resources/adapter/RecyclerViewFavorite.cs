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
using Newtonsoft.Json;

namespace MrPiattoClient.Resources.adapter
{
    class RecyclerViewFavoriteHolder : RecyclerView.ViewHolder
    {
        public TextView name, location, cuisine;
        public RatingBar ratingBar;
        public ImageView image;
        public Button button;
        public RecyclerViewFavoriteHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteRestaurantName);
            location = itemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteLocation);
            cuisine = itemView.FindViewById<TextView>(Resource.Id.cardviewFavoriteRestaurantCuisine);
            ratingBar = itemView.FindViewById<RatingBar>(Resource.Id.cardviewFavoriteRatingBar);
            image = itemView.FindViewById<ImageView>(Resource.Id.cardviewFavoriteImage);
            button = itemView.FindViewById<Button>(Resource.Id.buttonKnowMoreRes);
        }
    }
    class RecyclerViewFavoriteAdapter : RecyclerView.Adapter
    {
        private Context context;
        private List<CompleteRestaurant> favoriteRestaurant;
        public RecyclerViewFavoriteAdapter(List<CompleteRestaurant> favoriteRestaurant, Context context)
        {
            this.favoriteRestaurant = favoriteRestaurant;
            this.context = context;
        }

        public override int ItemCount
        {
            get { return favoriteRestaurant.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewFavoriteHolder viewHolder = holder as RecyclerViewFavoriteHolder;
            viewHolder.name.Text = favoriteRestaurant[position].name;
            viewHolder.location.Text = favoriteRestaurant[position].address;
            viewHolder.cuisine.Text = favoriteRestaurant[position].idcategoriesNavigation.category;
            viewHolder.ratingBar.Rating = (float)favoriteRestaurant[position].score;
            viewHolder.image.SetImageBitmap(ImageHelper.GetImageBitmapFromUrl(favoriteRestaurant[position].UrlMainFoto));
            viewHolder.button.Click += (sender, e) =>
            {
                Intent intent = new Intent(context, typeof(RestaurantActivity));
                intent.PutExtra("mainInfo", JsonConvert.SerializeObject(favoriteRestaurant[position]));
                context.StartActivity(intent);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_favorite, parent, false);
            return new RecyclerViewFavoriteHolder(itemView);
        }
    }
}