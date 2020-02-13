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
using Android.Support.Design.Widget;

namespace MrPiattoClient
{
    [Activity(Label = "SurveyActivity")]
    public class SurveyActivity : AppCompatActivity
    {
        Spinner spinnerA, spinnerB, spinnerC, spinnerD, spinnerE;
        List<Spinner> spinners = new List<Spinner>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_survey);

            InitToolbar();
            InitSpinners();
        }

        private void InitSpinners()
        {
            spinnerA = FindViewById<Spinner>(Resource.Id.SpinnerA);
            spinnerB = FindViewById<Spinner>(Resource.Id.SpinnerB);
            spinnerC = FindViewById<Spinner>(Resource.Id.SpinnerC);
            spinnerD = FindViewById<Spinner>(Resource.Id.SpinnerD);
            spinnerE = FindViewById<Spinner>(Resource.Id.SpinnerE);

            spinners.Add(spinnerB);
            spinners.Add(spinnerC);
            spinners.Add(spinnerD);
            spinners.Add(spinnerE);

            foreach (var spinner in spinners)
            {
                var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Rating, Android.Resource.Layout.SimpleListItem1);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner.Adapter = adapter;
            }

            var adapterName = ArrayAdapter.CreateFromResource(this, Resource.Array.WaitersName, Android.Resource.Layout.SimpleListItem1);
            adapterName.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerA.Adapter = adapterName;
        }
        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarSurvey);
            CollapsingToolbarLayout collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsingToolbarSurvey);
            AppBarLayout appBarLayout = FindViewById<AppBarLayout>(Resource.Id.appBarSurvey);
            toolbar.Title = "";
            SetSupportActionBar(toolbar);
            var isShow = false;
            var scrollRange = -1;

            appBarLayout.OffsetChanged += (sender, args) =>
            {
                if (scrollRange == -1)
                    scrollRange = appBarLayout.TotalScrollRange;
                if (scrollRange + args.VerticalOffset == 0)
                {
                    collapsingToolbar.Title = "Escribe una reseña";
                    isShow = true;
                }
                else if (isShow)
                {
                    collapsingToolbar.Title = string.Empty;
                    isShow = false;
                }
            };
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
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