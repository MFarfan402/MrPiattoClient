using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MrPiattoClient.Resources.adapter;
using AndroidX.RecyclerView.Widget;

namespace MrPiattoClient
{
    [Activity(Label = "MyReservationsActivity")]
    public class FragmentMyReservations : Fragment
    {
        RecyclerView recycler;
        RecyclerViewReservationAdapter adapter;
        List<Reservation> reservations = new List<Reservation>()
        {
            new Reservation("03/02/2020", "13:00", "3 personas", "Mr. Piatto Restaurant"),
            new Reservation("05/02/2020", "19:00", "6 personas", "Fonda de Doña Chona")
        };
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_myReservations, container, false);
            ReservationsOnScreen(view);
            return view;
        }

        private void ReservationsOnScreen(View view)
        {
            recycler = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewMyReservations);
            recycler.SetLayoutManager(new LinearLayoutManager(view.Context));
            recycler.SetItemAnimator(new DefaultItemAnimator());

            adapter = new RecyclerViewReservationAdapter(reservations, view.Context);
            recycler.SetAdapter(adapter);
        }
        public static FragmentMyReservations NewInstance()
        {
            return new FragmentMyReservations { Arguments = new Bundle() };
        }
    }
}