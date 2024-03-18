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
using System.Drawing;
using Java.Util;
using Android.Graphics.Drawables;
using static Java.Util.Jar.Attributes;
using Android.Content.Res;
using static Android.App.LauncherActivity;
using System.Threading.Tasks;


namespace BaiThi
{
    [Activity(Label = "Activity1")]
    public class TracNghiem : Activity
    {
        ImageView imgCauHoi;
        TextView txtCauHoi, txtCau;
        Button btnCau1, btnCau2, btnCau3, btnCau4, btnNext, btnEnd;
        List<string> CauTraLoi = new List<string>();
        List<string> CauHoi = new List<string>();
        List<string> ans = new List<string>();
        System.Random rnd = new System.Random();
        
        int cautraloi;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.tracNghiem);


            imgCauHoi = FindViewById<ImageView>(Resource.Id.imgCauHoi);
            txtCauHoi = FindViewById<TextView>(Resource.Id.CauHoi);
            btnCau1 = FindViewById<Button>(Resource.Id.btn1);
            btnCau2 = FindViewById<Button>(Resource.Id.btn2);
            btnCau3 = FindViewById<Button>(Resource.Id.btn3);
            btnCau4 = FindViewById<Button>(Resource.Id.btn4);
            btnNext = FindViewById<Button>(Resource.Id.btnNext);
            btnEnd = FindViewById<Button>(Resource.Id.btnKetThuc);
            txtCau = FindViewById<TextView>(Resource.Id.txtCau);
            LoadXML();
            btnCau1.Click += btnCau_Click;
            btnCau2.Click += btnCau_Click;
            btnCau3.Click += btnCau_Click;
            btnCau4.Click += btnCau_Click;
            HienCauHoi();
            btnEnd.Click += btnEnd_Click;
            
            btnNext.Click += btnNext_click;

        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (Intent.GetIntExtra("exam", 0) == 1)
            {
               
                
                    Intent it = new Intent(this, typeof(Diem)); // Khởi tạo Intent

                    //hien activity diem
                    Finish();
                    it.PutExtra("diem", Intent.GetIntExtra("diem",0));

                    StartActivity(it);

                    //0 la grammar, 1 la vocabulary


                

            }
            else
            {
                Finish();
                
            }
            
        }
        private void btnNext_click(object sender, EventArgs e)
        {
            if (Intent.GetIntExtra("cau", 0) < 10)
            {
                Intent.PutExtra("cau", Intent.GetIntExtra("cau",0) + 1);
                if (rnd.Next(0, 2) == 0)
                {
                    Intent.PutExtra("type", "voca");
                }
                else
                {
                    Intent.PutExtra("type", "gram");
                }

                Finish();
                OverridePendingTransition(0, 0);

                StartActivity(Intent);
                OverridePendingTransition(0, 0);
            }
            else
            {
                Intent it = new Intent(this, typeof(Diem)); // Khởi tạo Intent

                //hien activity diem
                Finish();
                it.PutExtra("diem", Intent.GetIntExtra("diem", 0));

                StartActivity(it);

                //0 la grammar, 1 la vocabulary


            }
        }
        private void HienCauHoi()
        {
            if (Intent.GetStringExtra("type") == "gram") { 
            cautraloi = rnd.Next(0, CauTraLoi.Count() - 1);
            
            
            
            string[] ds = new string[4];

            ds[0] = CauTraLoi[cautraloi];

            for (int i = 0; i < 3; i++)
            {
                ds[i+1] = ans[cautraloi*3 + i];
            }
            for (int i = 0; i < ds.Length - 1; ++i)
            {
                int r = rnd.Next(i, ds.Length - 1);
                (ds[r], ds[i]) = (ds[i], ds[r]);
            }
                imgCauHoi.Visibility = ViewStates.Gone;
            //string uri = "drawable/" + CauTraLoi[cautraloi];


                //int resourceid = (int)typeof(Resource.Drawable).GetField(CauTraLoi[cautraloi]).GetValue(null);


                //imgCauHoi.SetImageResource(resourceid);
            btnCau1.Text = ds[0];
            btnCau2.Text = ds[1];
            btnCau3.Text = ds[2];
            btnCau4.Text = ds[3];

            txtCauHoi.Text = CauHoi[cautraloi];
            }
            if (Intent.GetStringExtra("type") == "voca")
            {
                cautraloi = rnd.Next(0, CauTraLoi.Count() - 1);



                string[] ds = new string[4];

                ds[0] = CauTraLoi[cautraloi];

                for (int i = 0; i < 3; i++)
                {
                    ds[i + 1] = ans[cautraloi * 3 + i];
                }
                for (int i = 0; i < ds.Length - 1; ++i)
                {
                    int r = rnd.Next(i, ds.Length - 1);
                    (ds[r], ds[i]) = (ds[i], ds[r]);
                }


                try
                {
                    int resourceid = (int)typeof(Resource.Drawable).GetField(CauTraLoi[cautraloi]).GetValue(null);


                    imgCauHoi.SetImageResource(resourceid);
                    txtCauHoi.Text = "Con này là con gì?";
                }
                catch (Exception)
                {
                    imgCauHoi.Visibility = ViewStates.Gone;
                    txtCauHoi.Text = CauHoi[cautraloi];
                }

                
                btnCau1.Text = ds[0];
                btnCau2.Text = ds[1];
                btnCau3.Text = ds[2];
                btnCau4.Text = ds[3];
                
                
            }


        }
        private void LoadXML()
        {
            if (Intent.GetStringExtra("type") == "gram") { 
                XmlReader reader = XmlReader.Create(Assets.Open("Grammar.xml"));
                while (reader.Read())
                {

                
                        if (reader.Name.ToString() == "BaiTap")
                    {
                        CauHoi.Add(reader.ReadString());
                    }
                        
                        if (reader.Name.ToString() == "ans")
                    {
                        if (reader.GetAttribute("flag") == "1")
                        { CauTraLoi.Add(reader.ReadString()); }    
                        
                        if (reader.GetAttribute("flag") == "0")
                        { ans.Add(reader.ReadString()); }
                    
                    }
                }
            }
            if (Intent.GetStringExtra("type") == "voca")
            {
                XmlReader reader = XmlReader.Create(Assets.Open("Vocabulary.xml"));

                while (reader.Read())
                {
                    if (reader.Name.ToString() == "BaiTap")
                    {
                        CauHoi.Add(reader.ReadString());
                    }



                    if (reader.Name.ToString() == "ans")
                    {
                        if (reader.GetAttribute("flag") == "1")
                        { CauTraLoi.Add(reader.ReadString()); }

                        if (reader.GetAttribute("flag") == "0")
                        { ans.Add(reader.ReadString()); }
                        

                    }
                }
            }
        }
        
        private async void btnCau_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == CauTraLoi[cautraloi]) {
                btn.Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                if (Intent.GetIntExtra("exam",0) == 1) { Intent.PutExtra("diem", Intent.GetIntExtra("diem",0) + 1); }
                    
            }
            else {
                btn.Background.SetColorFilter(Android.Graphics.Color.Red, Android.Graphics.PorterDuff.Mode.Multiply);
            }
            if (Intent.GetIntExtra("exam", 0) == 1)
            {
                if (Intent.GetIntExtra("cau",0)<10)
                {
                    Intent.PutExtra("cau", Intent.GetIntExtra("cau",0) + 1);
                    if (rnd.Next(0,2)==0)
                    {
                        Intent.PutExtra("type", "voca");
                    }
                    else
                    {
                        Intent.PutExtra("type", "gram");
                    }
                    await Task.Delay(1000);
                    Finish();
                    OverridePendingTransition(0, 0);
                    
                    StartActivity(Intent);
                    OverridePendingTransition(0, 0);
                }
                else
                {
                    Intent it = new Intent(this, typeof(Diem)); // Khởi tạo Intent

                    //hien activity diem
                    await Task.Delay(1000);
                    Finish();
                    it.PutExtra("diem", Intent.GetIntExtra("diem",0));

                    StartActivity(it);

                    //0 la grammar, 1 la vocabulary


                }
                
            }
            else
            {
                await Task.Delay(1000);
                Finish();
                OverridePendingTransition(0, 0);
                StartActivity(Intent);
                OverridePendingTransition(0, 0);
            }
        }
    }
}