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

namespace MrPiattoClient
{
    [Activity(Label = "ComplaintActivity")]
    public class ComplaintActivity : AppCompatActivity
    {
        Spinner spinnerComplaint;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_complaint);

            InitToolbar();
            InitSppiner();

            // Create your application here
        }

        private void InitSppiner()
        {
            spinnerComplaint = FindViewById<Spinner>(Resource.Id.SpinnerComplaint);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Complaints, Android.Resource.Layout.SimpleListItem1);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerComplaint.Adapter = adapter;
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarComplaint);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Buzón de quejas";
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