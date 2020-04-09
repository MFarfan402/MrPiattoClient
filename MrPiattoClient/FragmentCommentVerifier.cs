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
using MrPiattoClient.Models;
using MrPiattoClient.Resources.utilities;

namespace MrPiattoClient
{
    public class FragmentCommentVerifier : Android.Support.V4.App.Fragment
    {
        public Context context;
        RecyclerView recycler;
        RecyclerViewCommentVerifierAdapter adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            APICaller caller = new APICaller();
            List<Comment> comments = caller.GetCommentsVerifier();
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