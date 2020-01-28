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
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient.Resources.adapter
{
    class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView imageView { get; set; }
        public RecyclerViewHolder(View itemView) : base(itemView)
        {
            imageView = itemView.FindViewById<ImageView>(Resource.Id.imageView1);
        }
    }

    class RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<string> dataSource;

        public RecyclerViewAdapter (List<string> dataSource) 
        { 
            this.dataSource = dataSource;
        }
        
        public override int ItemCount
        {
            get { return dataSource.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
            viewHolder.imageView.SetImageBitmap(ImageHelper.GetImageBitmapFromUrl(dataSource[position]));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_restaurantPhoto, parent, false);
            return new RecyclerViewHolder(itemView);
        }
    }
}