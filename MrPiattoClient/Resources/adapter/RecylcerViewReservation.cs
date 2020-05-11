using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MrPiattoClient.Models;
using MrPiattoClient.Resources.utilities;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MrPiattoClient.Resources.adapter
{
    
    class RecyclerViewReservationHolder : RecyclerView.ViewHolder
    {
        public TextView date, hour, quantity, restaurantName;
        public Android.Widget.Button buttonModify, buttonKnowMore;
        public ImageView image;
        public RecyclerViewReservationHolder(Android.Views.View itemView) : base(itemView)
        {
            date = itemView.FindViewById<TextView>(Resource.Id.cardviewDay);
            hour = itemView.FindViewById<TextView>(Resource.Id.cardviewHour);
            quantity = itemView.FindViewById<TextView>(Resource.Id.cardviewQuantity);
            restaurantName = itemView.FindViewById<TextView>(Resource.Id.cardviewReservationsName);
            image = itemView.FindViewById<ImageView>(Resource.Id.cardviewRestaurantImage);
            buttonModify = itemView.FindViewById<Android.Widget.Button>(Resource.Id.buttonModifyMyReservation);
            buttonKnowMore = itemView.FindViewById<Android.Widget.Button>(Resource.Id.buttonInfoMyReservation);
        }
    }

    class RecyclerViewReservationAdapter : RecyclerView.Adapter
    {
        private Context context;
        public List<Reservation> reservations;

        public RecyclerViewReservationAdapter(List<Reservation> reservations, Context context)
        {
            this.reservations = reservations;
            this.context = context;
        }

        public override int ItemCount
        {
            get { return reservations.Count(); }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewReservationHolder viewHolder = holder as RecyclerViewReservationHolder;
            viewHolder.date.Text = reservations[position].date.Date.ToString("dd-MM-yyyy");
            viewHolder.hour.Text = reservations[position].date.ToString("hh:mm:ss");
            viewHolder.quantity.Text = $"{reservations[position].amountOfPeople.ToString()} persona(s)";
            viewHolder.restaurantName.Text = reservations[position].idtableNavigation.idrestaurantNavigation.name;
            viewHolder.image.SetImageBitmap(ImageHelper.GetImageBitmapFromUrl(
                $"{APICaller.urlPhotos}{reservations[position].idtableNavigation.idrestaurant}/main.jpg"));
            viewHolder.buttonModify.Click += (sender, e) =>
            {
                Intent intent = new Intent(context, typeof(ModifyActivity));
                intent.PutExtra("JSONReservation", JsonConvert.SerializeObject(reservations[position]));
                context.StartActivity(intent);
            };
            viewHolder.buttonKnowMore.Click += (sender, e) =>
            {
                Dialog dialog = new Dialog(context);
                dialog.SetContentView(Resource.Layout.fragment_qr);
                dialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
                dialog.Show();

                ImageView qr = dialog.FindViewById<ImageView>(Resource.Id.qrImage);
                qr.SetImageBitmap(ImageHelper.GetImageBitmapFromUrl(reservations[position].url));

                Android.Widget.Button share = dialog.FindViewById<Android.Widget.Button>(Resource.Id.btnShareReservation);
                share.Click += async (sender, e) =>
                {
                    string share = $"Restaurante: {reservations[position].idtableNavigation.idrestaurantNavigation.name}\n" +
                        $"Fecha: {reservations[position].date.Date.ToString("dd-MM-yyyy")}\n" +
                        $"Hora: {reservations[position].date.ToString("hh:mm")}\n" +
                        $"Personas: {reservations[position].amountOfPeople}\n" +
                        $"Link QR: {reservations[position].url}";
                    await Share.RequestAsync(new ShareTextRequest
                    {
                        Text = share,
                        Title = "Compartir información"
                    });

                };

                Android.Widget.Button NFC = dialog.FindViewById<Android.Widget.Button>(Resource.Id.btnNFC);
                NFC.Click += (sender, e) =>
                {
                    Toast.MakeText(context, "NFC Activado", ToastLength.Short).Show();
                };

            };

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            Android.Views.View itemView = inflater.Inflate(Resource.Layout.cardview_myReservation, parent, false);
            return new RecyclerViewReservationHolder(itemView);
        }
    }

}