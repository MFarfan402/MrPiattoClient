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
    class RecyclerViewCuisineHolder : RecyclerView.ViewHolder
    {
        public TextView name;
        public ImageView image;
        public RecyclerViewCuisineHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewCuisineName);
            image = itemView.FindViewById<ImageView>(Resource.Id.cardviewCuisineImage);
        }
    }

    class RecyclerViewCuisineAdapter : RecyclerView.Adapter
    {
        private List<IdcategoriesNavigation> cuisines;

        public RecyclerViewCuisineAdapter(List<IdcategoriesNavigation> cuisines)
        {
            this.cuisines = cuisines;
        }

        public override int ItemCount
        {
            get { return cuisines.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewCuisineHolder viewHolder = holder as RecyclerViewCuisineHolder;
            viewHolder.name.Text = cuisines[position].category;
            viewHolder.image.SetImageBitmap(ImageHelper.GetImageBitmapFromUrl(cuisines[position].urlPhoto));

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_cuisine, parent, false);
            return new RecyclerViewCuisineHolder(itemView);
        }
    }
}