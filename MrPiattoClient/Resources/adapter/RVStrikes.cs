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
    public class RVStrikesHolder : RecyclerView.ViewHolder
    {
        public TextView name, date, reason;
        public RVStrikesHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.cardTagRestaurantInfo);
            date = itemView.FindViewById<TextView>(Resource.Id.cardTagDateInfo);
            reason = itemView.FindViewById<TextView>(Resource.Id.cardTagReasonInfo);
        }
    }

    class RVStrikesAdapter : RecyclerView.Adapter
    {
        private List<Strike> strikes;

        public RVStrikesAdapter(List<Strike> strikes)
        {
            this.strikes = strikes;
        }

        public override int ItemCount
        {
            get { return strikes.Count(); }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RVStrikesHolder viewHolder = holder as RVStrikesHolder;
            viewHolder.name.Text = strikes[position].RestaurantName;
            viewHolder.date.Text = strikes[position].StrikeDate;
            viewHolder.reason.Text = strikes[position].Reason;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_strike, parent, false);
            return new RVStrikesHolder(itemView);
        }
    }
}