using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;

namespace MrPiattoClient
{
    public class FragmentCommentVerifier : Android.Support.V4.App.Fragment
    {
        public Context context;
        RecyclerView recycler;
        RecyclerViewCommentVerifierAdapter adapter;
        List<Comment> comments = new List<Comment>()
        {
            new Comment(1,"Juanito","14/03/2020","El restaurante es !$%*"),
            new Comment(1,"Pedrito","13/03/2020","El restaurante es !$%*"),
            new Comment(2,"Chuchito","12/03/2020","El restaurante es !$%*"),
            new Comment(2,"Angelito","11/03/2020","El restaurante es !$%*"),
            new Comment(1,"Miguelito","10/03/2020","El restaurante es !$%*")
        };
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_reviewsVerifier, container, false);
            recycler = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerReviewVerifier);
            recycler.SetLayoutManager(new LinearLayoutManager(rootView.Context));
            recycler.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewCommentVerifierAdapter(comments, rootView.Context);
            recycler.SetAdapter(adapter);
            return rootView;

        }
        public static FragmentCommentVerifier NewInstance()
        {
            return new FragmentCommentVerifier { Arguments = new Bundle() };
        }
    }
}