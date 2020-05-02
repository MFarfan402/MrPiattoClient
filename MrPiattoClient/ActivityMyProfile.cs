﻿using System;
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
    [Activity(Label = "ActivityMyProfile")]
    public class ActivityMyProfile : AppCompatActivity
    {
        Spinner spinnerGender;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_personalData);
            InitToolbar();
            InitSpinners();
            InitListeners();

        }

        private void InitListeners()
        {
            Button save = FindViewById<Button>(Resource.Id.btnSavePersonalData);
            save.Click += delegate
            {
                Finish();
            };
        }

        private void InitSpinners()
        {
            spinnerGender = FindViewById<Spinner>(Resource.Id.spinnerGender2);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Gender, Android.Resource.Layout.SimpleListItem1);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerGender.Adapter = adapter;
        }

        private void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbarMyData);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Mi perfil";
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