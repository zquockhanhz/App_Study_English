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
    [Activity(Label = "Activity1")]
    public class Diem : Activity
    {
        TextView txtTongCau, txtCauDung, txtCauSai;
        Button btnEnd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here  

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Diem);
            txtTongCau = FindViewById<TextView>(Resource.Id.txtTongCau);
            txtCauDung = FindViewById<TextView>(Resource.Id.txtCauDung);
            txtCauSai = FindViewById<TextView>(Resource.Id.txtCauSai);
            btnEnd = FindViewById<Button>(Resource.Id.btnFinish);

            btnEnd.Click += btnEnd_click;

            txtTongCau.Text = "10";
            txtCauDung.Text = Intent.GetIntExtra("diem", 0).ToString();
            txtCauSai.Text = (10 - Intent.GetIntExtra("diem", 0)).ToString();
        }
        private void btnEnd_click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}