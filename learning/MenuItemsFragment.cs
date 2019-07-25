using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace learning
{
    public class MenuItemsFragment : Android.Support.V4.App.Fragment
    {
        private Button scanButton, addButton, showButton;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_menu, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            ((MainActivity)Activity).SupportActionBar.Title = "Package Menu";
            addButton = view.FindViewById<Button>(Resource.Id.AddButton);
            addButton.Click += (sender, args) =>
            {
                ((MainActivity)Activity).replaceFragment(new AddPackageFragment());
            };

            showButton = view.FindViewById<Button>(Resource.Id.ShowButton);
            showButton.Click += (sender, args) =>
            {
                ((MainActivity)Activity).replaceFragment(new ShowPackagesFragment());
            };
        }

    }
}
