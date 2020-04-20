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
    class RecyclerViewCommentVerifierHolder : RecyclerView.ViewHolder
    {
        public TextView userName, date, comment;
        public Button delete, noProblem;
        public RatingBar ratingBar;

        public RecyclerViewCommentVerifierHolder(View itemView) : base(itemView)
        {
            userName = itemView.FindViewById<TextView>(Resource.Id.userNameTextVerifier);
            date = itemView.FindViewById<TextView>(Resource.Id.ratingDateVerifier);
            comment = itemView.FindViewById<TextView>(Resource.Id.ratingCommentVerifier);
            delete = itemView.FindViewById<Button>(Resource.Id.deleteButtonVerifier);
            noProblem = itemView.FindViewById<Button>(Resource.Id.noProblemButtonVerifier);
            ratingBar = itemView.FindViewById<RatingBar>(Resource.Id.ratingBarCommentVerifier);
        }
    }

    class RecyclerViewCommentVerifierAdapter : RecyclerView.Adapter
    {
        private List<Comment> comments;
        private Context context;
        public RecyclerViewCommentVerifierAdapter(List<Comment> comments, Context context)
        {
            this.comments = comments;
            this.context = context;
        }

        public override int ItemCount => comments.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewCommentVerifierHolder viewHolder = holder as RecyclerViewCommentVerifierHolder;
            viewHolder.userName.Text = comments[position].name;
            viewHolder.date.Text = comments[position].date;
            viewHolder.comment.Text = comments[position].comment;
            viewHolder.ratingBar.Rating = (float)comments[position].rating;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(Resource.Layout.cardview_reviewVerifier, parent, false);
            return new RecyclerViewCommentVerifierHolder(view);

        }
    }
}