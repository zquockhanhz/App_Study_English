using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

using System.Xml;
using System.Collections.Generic;
using Android.Content;
using System.Linq;
using static Android.Service.Voice.VoiceInteractionSession;


namespace BaiThi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageButton Voca, Gram, Test, Wordle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Voca = FindViewById<ImageButton>(Resource.Id.btnVocabulary);
            Gram = FindViewById<ImageButton>(Resource.Id.btnGrammar);
            Test = FindViewById<ImageButton>(Resource.Id.btnTest); 
            Wordle = FindViewById<ImageButton>(Resource.Id.btnWordle);
            Wordle.Click += Wordle_click;
            Voca.Click += Voca_click;
            Gram.Click += Gram_click;
            Test.Click += Test_click;
        }
        private void Wordle_click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(Wordle)); // Khởi tạo Intent

            //0 la grammar, 1 la vocabulary
            
            StartActivity(it);
        }
        private void Test_click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(TracNghiem)); // Khởi tạo Intent

            //0 la grammar, 1 la vocabulary
            it.PutExtra("exam", 1);
            it.PutExtra("diem", 0);
            it.PutExtra("cau", 0);
            it.PutExtra("type", "voca");
            StartActivity(it);
        }
        private void Voca_click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(LyThuyet)); // Khởi tạo Intent

            //0 la grammar, 1 la vocabulary
            
            StartActivity(it);
        }
        private void Gram_click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(NguPhap)); // Khởi tạo Intent

            //0 la grammar, 1 la vocabulary
            
            StartActivity(it);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}