using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace learning
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            replaceFragment(new MenuItemsFragment());
        }

        /// <summary>
        /// Replaces the fragment.
        /// </summary>
        /// <param name="fragment">Fragment.</param>
        public void replaceFragment(Android.Support.V4.App.Fragment fragment)
        {
            String fragmentName = fragment.Class.Name;
            System.Diagnostics.Debug.WriteLine(string.Format("========== Fragment Name ======== {0}",
                fragmentName));

            Android.Support.V4.App.FragmentManager fragmentManager = this.SupportFragmentManager;

            bool popSuccess = fragmentManager.PopBackStackImmediate(fragmentName, 0);

            if (!popSuccess && fragmentManager.FindFragmentByTag(fragmentName) == null)
            {
                //Replace Fragment
                Android.Support.V4.App.FragmentTransaction ft = fragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.fragmentFrameLayout, fragment, fragmentName);
                ft.SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
                ft.AddToBackStack(fragmentName);
                ft.Commit();
            }
        }

        /// <summary>
        /// Ons the back pressed.
        /// </summary>
        public override void OnBackPressed()
        {
            //Check BackStack Fragment Count
            if (this.SupportFragmentManager.BackStackEntryCount == 1)
            {
                System.Diagnostics.Debug.WriteLine("Finish");
                Finish();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnBackPressed");
                base.OnBackPressed();
            }
        }
    }
}

