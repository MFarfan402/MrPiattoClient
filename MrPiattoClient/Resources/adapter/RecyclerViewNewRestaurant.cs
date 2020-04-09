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
    class RecyclerViewNewHolder : RecyclerView.ViewHolder
    {
        public TextView name, cuisine, location;
        public Button buttonAdd, buttonDeny;
        public RecyclerViewNewHolder(View view) : base(view)
        {
            name = view.FindViewById<TextView>(Resource.Id.cardviewVerifierNewName);
            cuisine = view.FindViewById<TextView>(Resource.Id.cardviewVerifierNewCuisine);
            location = view.FindViewById<TextView>(Resource.Id.cardviewVerifierNewLocation);
            buttonAdd = view.FindViewById<Button>(Resource.Id.buttonConfirmNew);
            buttonDeny = view.FindViewById<Button>(Resource.Id.buttonDeleteNew);
        }
    }

    class RecyclerViewNewAdapter : RecyclerView.Adapter
    {
        private List<Restaurant> restaurants;
        private Context context;
        public RecyclerViewNewAdapter(List<Restaurant> restaurants, Context context)
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
            RecyclerViewNewHolder viewHolder = holder as RecyclerViewNewHolder;
            viewHolder.name.Text = restaurants[position].Name;
            viewHolder.cuisine.Text = restaurants[position].Categories;
            viewHolder.location.Text = restaurants[position].Address;
            
            viewHolder.buttonAdd.Click += (sender, e) =>
            {
                Intent intent = new Intent(context, typeof(ActivityAddRestaurant));
                context.StartActivity(intent);
            };
            /*
            viewHolder.buttonDeny.Click += (sender, e) =>
            {

            };
            */
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_newRestaurant, parent, false);
            return new RecyclerViewNewHolder(itemView);
        }
    }
}