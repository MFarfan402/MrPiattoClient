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
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;
using MrPiattoClient.Models;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    [Activity(Label = "ActivityRestaurantRatings")]
    public class ActivityRestaurantRatings : AppCompatActivity
    {
        APICaller API = new APICaller();
        List<Comment> comments = new List<Comment>();
        RecyclerView recyclerView;
        RecyclerViewRatingCommentAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ratings);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarRatings);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Calificaciones y reseñas";
            InflateData();
            GetComments();
            PutCommentsOnScreen();
        }

        private void GetComments()
        {
            List<Survey> surveys = API.GetRatingAndComments(Intent.GetIntExtra("idRestaurant", 0));
            foreach(var s in surveys)
            {
                comments.Add(new Comment(s.idcomment, s.generalScore, "Usuario", s.idcommentNavigation.date.ToString(), s.idcommentNavigation.comment));
            }
        }

        private void InflateData()
        {
            List<int> bars = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(Intent.GetStringExtra("bars"));
            List<float> ratings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<float>>(Intent.GetStringExtra("ratings"));

            TextView generalRating = FindViewById<TextView>(Resource.Id.generalRating);
            RatingBar rating = FindViewById<RatingBar>(Resource.Id.restaurantRatingSmall);
            ProgressBar pbar1 = FindViewById<ProgressBar>(Resource.Id.pbar1);
            ProgressBar pbar2 = FindViewById<ProgressBar>(Resource.Id.pbar2);
            ProgressBar pbar3 = FindViewById<ProgressBar>(Resource.Id.pbar3);
            ProgressBar pbar4 = FindViewById<ProgressBar>(Resource.Id.pbar4);
            ProgressBar pbar5 = FindViewById<ProgressBar>(Resource.Id.pbar5);
            rating.Rating = ratings[3];
            generalRating.Text = ratings[3].ToString();
            pbar1.Progress = bars[0];
            pbar2.Progress = bars[1];
            pbar3.Progress = bars[2];
            pbar4.Progress = bars[3];
            pbar5.Progress = bars[4];

            TextView foodRating = FindViewById<TextView>(Resource.Id.ratingFood);
            TextView comfortRating = FindViewById<TextView>(Resource.Id.ratingEnvironment);
            TextView serviceRating = FindViewById<TextView>(Resource.Id.ratingService);
            foodRating.Text = ratings[0].ToString();
            comfortRating.Text = ratings[1].ToString();
            serviceRating.Text = ratings[2].ToString();

        }

        private void PutCommentsOnScreen()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewRatingComment);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewRatingCommentAdapter(comments);
            recyclerView.SetAdapter(adapter);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}