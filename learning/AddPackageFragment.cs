
using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace learning
{
    public class AddPackageFragment : Android.Support.V4.App.Fragment
    {
        private EditText barcode_field_input, width_field, height_field, depdth_field;
        private Button save_button, reset_button;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_add_package, container, false);
            return view;

        }


        /// <summary>
        /// Saves the button action async.
        /// </summary>
        /// <returns>The button action async.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private async Task SaveButtonClickAsync(System.Object sender, EventArgs e)
        {
            Package package = new Package()
            {
                Barcode = barcode_field_input.Text,
                Height = height_field.Text,
                Width = width_field.Text,
                Depth = depdth_field.Text
            };
            string status = await DBHelper.GetInstance().AddNewPackageAsync(package);
            Toast.MakeText(this.Activity, status, ToastLength.Short).Show();

            if(status.Contains("Added Successfully"))
            {
                barcode_field_input.Text = string.Empty;
                width_field.Text = string.Empty;
                height_field.Text = string.Empty;
                depdth_field.Text = string.Empty;
                barcode_field_input.RequestFocus();
            }
        }


        /// <summary>
        /// Rests the button action.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void RestButtonClick(System.Object sender, EventArgs e)
        {
            barcode_field_input.Text = string.Empty;
            width_field.Text = string.Empty;
            height_field.Text = string.Empty;
            depdth_field.Text = string.Empty;

            Toast.MakeText(this.Activity, "Reset Successfully", ToastLength.Short).Show();
            barcode_field_input.RequestFocus();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            ((MainActivity)Activity).SupportActionBar.Title = "Add Package";

            barcode_field_input = view.FindViewById<EditText>(Resource.Id.barcode_field_input);

            width_field = view.FindViewById<EditText>(Resource.Id.width_field);
            width_field.SetFilters(new IInputFilter[] { new DecimalInputFilter() });
            height_field = view.FindViewById<EditText>(Resource.Id.height_field);
            height_field.SetFilters(new IInputFilter[] { new DecimalInputFilter() });
            depdth_field = view.FindViewById<EditText>(Resource.Id.depdth_field);
            depdth_field.SetFilters(new IInputFilter[] { new DecimalInputFilter() });

            save_button = view.FindViewById<Button>(Resource.Id.save_button);
            save_button.Click += async (sender, args) =>
            {
                await SaveButtonClickAsync(sender, args);
            };
            reset_button = view.FindViewById<Button>(Resource.Id.reset_button);
            reset_button.Click += RestButtonClick;
        }

        /// <summary>
        /// Decimal input filter.
        /// </summary>
        public class DecimalInputFilter : Java.Lang.Object, Android.Text.IInputFilter
        {
            string regex = "[-+]?^[0-9]+(.[0-9]{0,1})?$";
            public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(dest.ToString(), regex) || dest.ToString().Equals(""))
                {
                    return new Java.Lang.String(source.ToString());
                }
                return new Java.Lang.String(string.Empty);
            }
        }

    }
}
