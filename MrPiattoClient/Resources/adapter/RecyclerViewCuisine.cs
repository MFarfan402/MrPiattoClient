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
using AndroidX.RecyclerView.Widget;
using MrPiattoClient.Models;
using MrPiattoClient.Resources.utilities;
using Android.Support.V4.App;

namespace MrPiattoClient.Resources.adapter
{
    class RecyclerViewCuisineHolder : RecyclerView.ViewHolder
    {
        public TextView name;
        public ImageView image;
        public View Item;
        public RecyclerViewCuisineHolder(View itemView) : base(itemView)
        {
            Item = itemView;
            name = itemView.FindViewById<TextView>(Resource.Id.cardviewCuisineName);
            image = itemView.FindViewById<ImageView>(Resource.Id.cardviewCuisineImage);
        }
    }

    class RecyclerViewCuisineAdapter : RecyclerView.Adapter
    {
        private List<IdcategoriesNavigation> cuisines;
        private Context context;
        private RecyclerView recycler;
        private FragmentActivity activity;

        public RecyclerViewCuisineAdapter(List<IdcategoriesNavigation> cuisines, Context context, RecyclerView recycler, FragmentActivity activity)
        {
            this.cuisines = cuisines;
            this.context = context;
            this.recycler = recycler;
            this.activity = activity;
        }

        public override int ItemCount
        {
            get { return cuisines.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewCuisineHolder viewHolder = holder as RecyclerViewCuisineHolder;
            viewHolder.name.Text = cuisines[position].category;
            viewHolder.image.SetImageBitmap(ImageHelper.GetImageBitmapFromUrl(cuisines[position].urlPhoto));
            viewHolder.Item.Click -= MakeToast;
            viewHolder.Item.Click += MakeToast;
        }

        private void MakeToast(object sender, EventArgs e)
        {
            int position = recycler.GetChildAdapterPosition((View)sender);

            Android.Support.V4.App.Fragment fragment = FragmentSearch.NewInstance();
            Bundle args = new Bundle();
            args.PutString("search", cuisines[position].category);
            fragment.Arguments = args;
            activity.SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.frameMainContent, fragment)
                .Commit();

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.cardview_cuisine, parent, false);
            return new RecyclerViewCuisineHolder(itemView);
        }
    }
}