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
using static Android.Service.Voice.VoiceInteractionSession;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace BaiThi
{
    [Activity(Label = "LyThuyet")]
    public class LyThuyet : Activity
    {
        EditText editText;
        ListView listView;
        List<string> mydataEn = new List<string>();
        List<string> mydataVn = new List<string>();
        ArrayAdapter adapter;
        Button Test;
   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LyThuyet);

            editText = FindViewById<EditText>(Resource.Id.editText);
            listView = FindViewById<ListView>(Resource.Id.listView);
            Test= FindViewById<Button>(Resource.Id.Test);
            LoadXML();
            adapter = new ArrayAdapter<string>(
this,
Resource.Layout.support_simple_spinner_dropdown_item, mydataEn);
            listView.Adapter = adapter;
            editText.TextChanged += EditText_TextChanged;
            listView.ItemClick += ListView_ItemClick;
            Test.Click += Voca_click;
        }
        private void Voca_click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(TracNghiem)); // Khởi tạo Intent

            //0 la grammar, 1 la vocabulary
            it.PutExtra("type", "voca");
            StartActivity(it);
        }


        private void ListView_ItemClick(object sender,
AdapterView.ItemClickEventArgs e)
        {
            Intent it = new Intent(this, typeof(DonLiThuyet)); // Khởi tạo Intent
            it.PutExtra("en", mydataEn[e.Position]); // truyền giá trị tiếng Anh
            it.PutExtra("vn", mydataVn[e.Position]);
            StartActivity(it); // Mở Activity mới
        }
        private void EditText_TextChanged(object sender,
Android.Text.TextChangedEventArgs e)
        {
            mydataEn = mydataEn.Where(x => x.Contains(e.Text.ToString())).ToList();
            adapter.Clear();
            adapter = new ArrayAdapter<string>(
            this, Resource.Layout.support_simple_spinner_dropdown_item,
            mydataEn);
            listView.Adapter = adapter;
        }

        private void LoadXML()
        {
            XmlReader reader = XmlReader.Create(Assets.Open("mydata.xml"));
            while (reader.Read())
            {
                if (reader.Name.ToString() == "en")
                    mydataEn.Add(reader.ReadString());
                if (reader.Name.ToString() == "vn")
                    mydataVn.Add(reader.ReadString());
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
