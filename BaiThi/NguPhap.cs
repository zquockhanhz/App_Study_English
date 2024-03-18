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
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace BaiThi
{
    [Activity(Label = "NguPhap")]
    public class NguPhap : Activity
    
    {
       
        ListView listView;
        List<string> Ten = new List<string>();
        List<string> Chinh = new List<string>();
        List<string> VD = new List<string>();
        
        ArrayAdapter adapter;
        Button Test;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.GrammarLT);

            
            listView = FindViewById<ListView>(Resource.Id.listView);
            Test = FindViewById<Button>(Resource.Id.Test);
            LoadXML();
            adapter = new ArrayAdapter<string>(
this,
Resource.Layout.support_simple_spinner_dropdown_item, Ten);
            listView.Adapter = adapter;
            
            listView.ItemClick += ListView_ItemClick;
            Test.Click += Gram_click;
        }
        private void Gram_click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(TracNghiem)); // Khởi tạo Intent

            //0 la grammar, 1 la vocabulary
            it.PutExtra("type", "gram");
            StartActivity(it);
        }


        private void ListView_ItemClick(object sender,
AdapterView.ItemClickEventArgs e)
        {
            Intent it = new Intent(this, typeof(GrammarLTDon)); // Khởi tạo Intent
            it.PutExtra("Ten", Ten[e.Position]); // truyền giá trị tiếng Anh
            it.PutExtra("Chinh", Chinh[e.Position]);
            it.PutExtra("VD1", VD[e.Position*2]);
            it.PutExtra("VD2", VD[e.Position*2+1]);
            StartActivity(it); // Mở Activity mới
        }
        

        private void LoadXML()
        {
            XmlReader reader = XmlReader.Create(Assets.Open("GrammarLT.xml"));
            while (reader.Read())
            {
                if (reader.Name.ToString() == "Ten")
                    Ten.Add(reader.ReadString());
                if (reader.Name.ToString() == "Chinh")
                    Chinh.Add(reader.ReadString());
                if (reader.Name.ToString() == "VD")
                    VD.Add(reader.ReadString());
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}