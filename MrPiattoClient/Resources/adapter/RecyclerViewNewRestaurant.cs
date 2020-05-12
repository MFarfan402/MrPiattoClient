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
        APICaller API = new APICaller();
        private List<NewRestaurant> restaurants;
        private Context context;
        public RecyclerViewNewAdapter(List<NewRestaurant> restaurants, Context context)
        {
            this.restaurants = restaurants;
            this.context = context;
        }

        public override int ItemCount
        {
            get { return restaurants.Count(); }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewNewHolder viewHolder = holder as RecyclerViewNewHolder;
            viewHolder.name.Text = restaurants[position].name;
            viewHolder.cuisine.Text = restaurants[position].phone;
            viewHolder.location.Text = restaurants[position].address;
            
            viewHolder.buttonAdd.Click += (sender, e) =>
            {
                var password = API.AcceptRestaurant(restaurants[position].idrestaurant);
                if (password.Length == 5)
                {
                    
                    Intent intent = new Intent(context, typeof(ActivityAddRestaurant));
                    intent.PutExtra("password", password);
                    intent.PutExtra("JSONRes", JsonConvert.SerializeObject(restaurants[position]));
                    context.StartActivity(intent);
                    restaurants.RemoveAt(position);
                    NotifyDataSetChanged();
                    NotifyItemChanged(position);
                }
            };
            viewHolder.buttonDeny.Click += (sender, e) =>
            {
                if (API.DenyRestaurant(restaurants[position].idrestaurant))
                {
                    restaurants.RemoveAt(position);
                    NotifyDataSetChanged();
                    NotifyItemChanged(position);
                }
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_newRestaurant, parent, false);
            return new RecyclerViewNewHolder(itemView);
        }
    }
}