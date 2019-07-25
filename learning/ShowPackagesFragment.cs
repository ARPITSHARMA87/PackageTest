using System.Collections.Generic;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace learning
{
    public class ShowPackagesFragment : Android.Support.V4.App.Fragment
    {
        private RecyclerView recyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_show_package, container, false);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            ((MainActivity)Activity).SupportActionBar.Title = "Show Packages";

            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.HasFixedSize = true;

            LinearLayoutManager layoutManager = new LinearLayoutManager(Context);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));

            Activity.RunOnUiThread(async () =>
            {
                List<Package> packages = await DBHelper.GetInstance().GetAllPackageAsync();
                System.Diagnostics.Debug.WriteLine(packages.Count);
                recyclerView.SetAdapter(new PackageAdapter(packages));
            });
        }
        /// <summary>
        /// Package adapter.
        /// </summary>
        public class PackageAdapter : RecyclerView.Adapter
        {
            private List<Package> sourceList;
            public PackageAdapter(List<Package> list)
            {
                this.sourceList = list;
            }
            public override int ItemCount => sourceList.Count;
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).
                            Inflate(Resource.Layout.view_package_item, parent, false);
                return new PackageViewHolder(itemView);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                (holder as PackageViewHolder).Bind(sourceList[position]);
            }
        }

        /// <summary>
        /// Package view holder.
        /// </summary>
        public class PackageViewHolder : RecyclerView.ViewHolder
        {
            public TextView BarcodeTextView, DimmTextView;
            public PackageViewHolder(View itemView) : base(itemView)
            {
                BarcodeTextView = itemView.FindViewById<TextView>(Resource.Id.BarcodeTextView);
                DimmTextView = itemView.FindViewById<TextView>(Resource.Id.DimmTextView);
            }

            public void Bind(Package package)
            {
                BarcodeTextView.Text = package.Barcode;
                DimmTextView.Text = string.Format("Dimm ({0} x {1} x {2})", package.Width, package.Height, package.Depth);
            }
        }


    }
}
