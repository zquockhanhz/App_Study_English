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
    [Activity(Label = "GrammarLTDon")]
    public class GrammarLTDon : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TextView txtTen, txtChinh, txtVD1, txtVD2;
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.GrammarLTDon);

            txtTen = FindViewById<TextView>(Resource.Id.txtTen);
            txtChinh = FindViewById<TextView>(Resource.Id.txtChinh);
            txtVD1 = FindViewById<TextView>(Resource.Id.txtVD1);
            txtVD2 = FindViewById<TextView>(Resource.Id.txtVD2);


            txtTen.Text = Intent.GetStringExtra("Ten");
            txtChinh.Text = Intent.GetStringExtra("Chinh");
            txtVD1.Text = Intent.GetStringExtra("VD1");
            txtVD2.Text = Intent.GetStringExtra("VD2");
        }
    }
}