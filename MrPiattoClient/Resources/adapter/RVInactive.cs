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
    class RVInactiveHolder : RecyclerView.ViewHolder
    {
        public TextView name, lastConnection, phone;
        public ImageView image;
        public Button button;
        public RVInactiveHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewDeleteName);
            lastConnection = itemView.FindViewById<TextView>(Resource.Id.cardviewDeleteConnection);
            phone = itemView.FindViewById<TextView>(Resource.Id.cardviewVerifierPhone);
            image = itemView.FindViewById<ImageView>(Resource.Id.cardviewDeleteRestaurant);
            button = itemView.FindViewById<Button>(Resource.Id.buttonDeleteSystem);
        }
    }

    class RVInactiveAdapter : RecyclerView.Adapter
    {
        APICaller API = new APICaller();
        Context context;
        private List<WarningRestaurant> restaurants;

        public RVInactiveAdapter(Context context, List<WarningRestaurant> restaurants)
        {
            this.context = context;
            this.restaurants = restaurants;
        }

        public override int ItemCount => restaurants.Count();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RVInactiveHolder viewHolder = holder as RVInactiveHolder;
            viewHolder.name.Text = restaurants[position].name;
            viewHolder.lastConnection.Text = $"Última conexión: {restaurants[position].lastLogin.ToString("dd-MM-yyyy")}";
            viewHolder.phone.Text = $"Teléfono: {restaurants[position].phone}";
            viewHolder.image.SetImageBitmap(ImageHelper.GetImageBitmapFromUrl(restaurants[position].UrlMainFoto));
            viewHolder.button.Click += delegate
            {
                API.DeleteRestaurant(restaurants[position].idrestaurant);
                restaurants.RemoveAt(position);
                NotifyDataSetChanged();
                Toast.MakeText(context, "El restaurante ha sido eliminado.", ToastLength.Long).Show();
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View item = inflater.Inflate(Resource.Layout.cardview_deleteRestaurant, parent, false);
            return new RVInactiveHolder(item);
        }
    }
}