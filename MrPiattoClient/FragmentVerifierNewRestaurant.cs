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
using Fragment = Android.Support.V4.App.Fragment;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;
using MrPiattoClient.Models;

namespace MrPiattoClient
{
    public class FragmentVerifierNewRestaurant : Fragment
    {
        List<Restaurant> main = new List<Restaurant>()
        {
            new Restaurant(1, 5, "Mr. Piatto Restaurant", "Vista a la Campina 5414, Cerro del Tesoro, 45608, Tlaquepaque, Jal.", "TAILANDESA"),
            new Restaurant(2, 5, "Tacos de DonPerro", "Nueva Escocia 12641, Afuera del CETI, 45608, Gudalajara, Jal.", "DESCONOCIDO"),
            new Restaurant(3, 4, "Fonda de doña chona", "Vista a la Campina 5414, Cerro del Tesoro, 45608", "MEXICANA")
        };
        RecyclerView recycler;
        RecyclerViewNewAdapter adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_addRestaurant, container, false);
            recycler = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerAddRestaurant);
            recycler.SetLayoutManager(new LinearLayoutManager(rootView.Context));
            recycler.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewNewAdapter(main, rootView.Context);
            recycler.SetAdapter(adapter);
            return rootView;
        }

        public static FragmentVerifierNewRestaurant NewInstance()
        {
            return new FragmentVerifierNewRestaurant { Arguments = new Bundle() };
        }
    }

    
}