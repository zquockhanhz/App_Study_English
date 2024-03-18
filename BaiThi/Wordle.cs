using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace BaiThi
{
    [Activity(Label = "Activity1")]
    public class Wordle : Activity
    {
        //TextView txtA, txtB, txtC, txtD, txtE, txtF, txtG, txtH, txtI, txtK, txtJ, txtL, txtM, txtN, txtO, txtP, txtQ, txtR, txtS, txtT, txtU, txtV, txtW, txtX, txtY, txtZ, txtDel;
        Button btnSubmit, btnBack;

        int trynum =0;
        System.Random rnd = new System.Random();

        private List<TextView> Keyboard  = new List<TextView>();

        TextView txtTest ,txtDel;
        string chosenword;

        private List<TextView> ScreenView = new List<TextView>();
        List<string> words = new List<string>();
        //TextView txtTry1_1, txtTry2_1, txtTry3_1, txtTry4_1, txtTry5_1, txtTry1_2, txtTry2_2, txtTry3_2, txtTry4_2, txtTry5_2, txtTry1_3, txtTry2_3, txtTry3_3, txtTry4_3, txtTry5_3, txtTry1_4, txtTry2_4, txtTry3_4, txtTry4_4, txtTry5_4, txtTry1_5, txtTry2_5, txtTry3_5, txtTry4_5, txtTry5_5;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.wordle);
            //Ini Keyboard
            
            LoadXML();
            
                chosenword = words[rnd.Next(0, words.Count()-1)];
            
            char keybrd = 'A';
            for (int i = 0; i < 26; i++)
            {
                Keyboard.Add((TextView)FindViewById(Resources.GetIdentifier("txt"+ keybrd, "id", PackageName)));
                Keyboard[i].Click+=txtKeyboard_click;
                keybrd++;
            }
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    ScreenView.Add((TextView)FindViewById(Resources.GetIdentifier("try"+i.ToString()+"_"+j.ToString(), "id", PackageName)));
                }
                
            }

            txtTest=FindViewById<TextView>(Resource.Id.txtTest);
            btnSubmit= FindViewById<Button>(Resource.Id.btnSubmit);
            btnBack = FindViewById<Button>(Resource.Id.btnBack);
            txtDel = FindViewById<TextView>(Resource.Id.txtDel);
            txtTest.Text = chosenword;
            btnSubmit.Click += btnSubmit_click;
            btnBack.Click += btnEnd_click;




            txtDel.Click += Delete_click;
            
        }
        private void btnEnd_click(object sender, EventArgs e)
        {
            Finish();
        }
        private void LoadXML()
        {
            XmlReader reader = XmlReader.Create(Assets.Open("Wordle.xml"));
            while (reader.Read())
            {
                if (reader.Name.ToString() == "word")
                {
                    words.Add(reader.ReadString());
                }
            }
        }

        private void btnSubmit_click(object sender, EventArgs e)
        {
            if (trynum < 5&& ScreenView[5 * trynum + 4].Text!="_")
            {

            
            for (int i = 0; i < 5; i++)
            {


                if (ScreenView[5 * trynum + i].Text == chosenword[i].ToString())
                {

                    ScreenView[5 * trynum + i].Background.SetColorFilter(Android.Graphics.Color.Green, Android.Graphics.PorterDuff.Mode.Multiply);
                }
                else
                {
                    if (chosenword.Contains(ScreenView[5 * trynum + i].Text[0]))
                    {
                        int counter=0;
                        int correct=0;
                        var chosenwordcount = chosenword.Count(x => x == ScreenView[5 * trynum + i].Text[0]);
                        //sushi  shikl
                        for (int j = 0; j < 5; j++)
                        {
                            if (ScreenView[5 * trynum + i].Text == ScreenView[5 * trynum + j].Text)
                            {
                                counter++;

                                if (ScreenView[5 * trynum + j].Text == chosenword[j].ToString())
                                {
                                    correct++;
                                }
                            }
                            
                        }
                        if (correct < chosenwordcount)
                        {
                            
                            ScreenView[5 * trynum + i].Background.SetColorFilter(Android.Graphics.Color.LightYellow, Android.Graphics.PorterDuff.Mode.Multiply);
                        }
                        else
                        {
                            
                            ScreenView[5 * trynum + i].Background.SetColorFilter(Android.Graphics.Color.Gray, Android.Graphics.PorterDuff.Mode.Multiply);
                        }
                    }
                    else
                    {
                        ScreenView[5 * trynum + i].Background.SetColorFilter(Android.Graphics.Color.Gray, Android.Graphics.PorterDuff.Mode.Multiply);
                    }
                    
                }
            }
            if (chosenword == (ScreenView[5 * trynum + 0].Text+ ScreenView[5 * trynum + 1].Text+ ScreenView[5 * trynum + 2].Text+ ScreenView[5 * trynum + 3].Text+ ScreenView[5 * trynum + 4].Text))
            {
                txtTest.Text = "win";
                    Endgame();
            }
            
                
            
            }
            if (trynum>=5)
            {
                txtTest.Text = "lose";
                Endgame();
            }
            else
            {
                trynum++;
            }
        }

        private async void Endgame()
        {
            await Task.Delay(3000);
            Finish();
        }

        private void txtKeyboard_click(object sender, EventArgs e)
        {
            TextView btn = sender as TextView;
            if (trynum<5)
            {

            
                for (int i = 0; i < 5; i++)
                {
                    if (ScreenView[5 * trynum +i].Text=="_")
                    {
                    
                    ScreenView[5 * trynum + i].Text = btn.Text;
                    break;
                    }
                }
            }
                
           
        }
        private void Delete_click(object sender, EventArgs e)
        {
            if (trynum < 5)
            {
                for (int i = 5; i >= 0; i--)
                {
                    if (ScreenView[5 * trynum + i].Text != "_")
                    {

                        ScreenView[5 * trynum + i].Text = "_";
                        break;
                    }
                }
            }
        }
    }
}