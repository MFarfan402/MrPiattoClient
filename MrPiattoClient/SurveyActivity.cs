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
using MrPiattoClient.Resources.utilities;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MrPiattoClient.Models;

namespace MrPiattoClient
{
    [Activity(Label = "SurveyActivity")]
    public class SurveyActivity : AppCompatActivity
    {
        Spinner spinnerA, spinnerB, spinnerC, spinnerD, spinnerE;
        EditText userComment;
        APICaller API = new APICaller();
        List<Spinner> spinners = new List<Spinner>();
        Button button;
        int idRestaurant;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_survey);
            idRestaurant = Intent.GetIntExtra("idRestaurant", 0);
            InitToolbar();
            InitSpinners();
            InitListeners();
        }

        private void InitListeners()
        {
            button = FindViewById<Button>(Resource.Id.buttonSurveyComplete);
            userComment = FindViewById<EditText>(Resource.Id.editTextComment);
            button.Click += async (sender, e) =>
            {
                if (userComment.Text.Length == 0)
                {
                    Toast.MakeText(this, "Por favor, agrega un comentario", ToastLength.Long).Show();
                }
                else
                {
                    if(CheckBadWords(userComment.Text))
                    {
                        List<float> scores = new List<float>();
                        int idWaiter = GetIDWaiter(spinnerA.SelectedItem.ToString());
                        scores.Add(int.Parse(spinnerB.SelectedItem.ToString()) / 2);
                        scores.Add(int.Parse(spinnerC.SelectedItem.ToString()) / 2);
                        scores.Add(int.Parse(spinnerD.SelectedItem.ToString()) / 2);
                        scores.Add(int.Parse(spinnerE.SelectedItem.ToString()) / 2);
                        scores.Add(scores.Average());

                        var msg = await API.PostSurvey(idWaiter, scores, userComment.Text.ToString(), idRestaurant);
                        Toast.MakeText(this, msg, ToastLength.Long).Show();
                        Finish();
                    }
                    else
                        Toast.MakeText(this, "No debes incluir groserías o spam en tu comentario.", ToastLength.Long).Show();
                }
            };
        }

        private int GetIDWaiter(string waiter)
        {
            Regex regex = new Regex(@"\b\w+(?=\.)");
            return int.Parse(regex.Match(waiter).ToString());
        }

        private bool CheckBadWords(string comment)
        {
            List<string> badWords = new List<string>()
            {
                "puto", "pendejo", "zorro", "joto", "golfa", "verga", "baboso", "pinche", "estupido", "meco",
                "www", ".com", "http"
            };
            string pattern = @$"\b(1)(s?)\b";
            string patternAux = @$"\b(1)(s?)\b";
            RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            Regex regex;
            foreach(var badWord in badWords)
            {
                pattern = pattern.Replace("1", badWord);
                regex = new Regex(pattern, options);
                if (regex.IsMatch(comment))
                    return false;
                pattern = patternAux;
            }
            return true;
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

            List<Waiter> waiters = API.GetWaiters(idRestaurant);
            List<string> stringWaiters = new List<string>();
            foreach (var w in waiters)
                stringWaiters.Add(w.ToString());

            var adapterName = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, stringWaiters);
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