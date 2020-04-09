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
    
    class RecyclerViewRatingCommentHolder : RecyclerView.ViewHolder
    {
        public TextView name, date, comment;
        public RatingBar rating;
        public Button buttonReport;
        public RecyclerViewRatingCommentHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.userNameText);
            date = itemView.FindViewById<TextView>(Resource.Id.ratingDate);
            comment = itemView.FindViewById<TextView>(Resource.Id.ratingComment);
            rating = itemView.FindViewById<RatingBar>(Resource.Id.ratingBarComment);

            buttonReport = itemView.FindViewById<Button>(Resource.Id.reportButton);
        }
    }

    class RecyclerViewRatingCommentAdapter : RecyclerView.Adapter
    {
        private List<Comment> comments;
        public RecyclerViewRatingCommentAdapter(List<Comment> comments)
        {
            this.comments = comments;
        }

        public override int ItemCount
        {
            get { return comments.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewRatingCommentHolder viewHolder = holder as RecyclerViewRatingCommentHolder;
            viewHolder.name.Text = comments[position].name;
            viewHolder.date.Text = comments[position].date;
            viewHolder.comment.Text = comments[position].comment;
            viewHolder.rating.Rating = comments[position].rating;
            viewHolder.buttonReport.Click += delegate
            {
                Console.WriteLine(position);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_review, parent, false);
            return new RecyclerViewRatingCommentHolder(itemView);
        }
    }
}