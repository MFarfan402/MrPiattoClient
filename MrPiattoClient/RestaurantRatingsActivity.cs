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

namespace MrPiattoClient
{
    [Activity(Label = "RestaurantRatingsActivity")]
    public class RestaurantRatingsActivity : AppCompatActivity
    {
        List<Comment> comments = new List<Comment>()
        {
            new Comment(1, "Juanito", "12/12/2012", "La carne estaba medio mala"),
            new Comment(2, "Pedrito", "11/11/2011", "La carne estaba malisima. Malas bebidas"),
            new Comment(3, "Chuchito", "10/10/2010", "Buen servicio pero aun hay mucho en que mejorar"),
            new Comment(4, "Miguelito", "9/19/2019", "Lo mejor de lo mejor, excepto por la limpieza en los baños."),
            new Comment(5, "Angelito", "8/8/2018", "OBVIO que los M&Ms se van a graduar. Lo dije y lo apruebo.")
        };
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

            PutCommentsOnScreen();
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