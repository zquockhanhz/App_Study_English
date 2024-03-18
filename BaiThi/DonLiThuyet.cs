using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiThi
{
    [Activity(Label = "DonLiThuyet")]
    public class DonLiThuyet : Activity
    {
        TextView textViewEn, textViewVn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.DonLT);

            textViewEn = FindViewById<TextView>(Resource.Id.textViewEn);
            textViewVn = FindViewById<TextView>(Resource.Id.textViewVn);
            textViewEn.Text = Intent.GetStringExtra("en");
            textViewVn.Text = Intent.GetStringExtra("vn");
        }
    }
}