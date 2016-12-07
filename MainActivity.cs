using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXing.Mobile;
using RestSharp;
using System.Net;
using projekt.DTO;
using Android.App.Usage;

namespace projekt
{
    [Activity(Label = "projekt", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public ProgressBar kolko;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Button zaloguj = FindViewById<Button>(Resource.Id.Zaloguj);
            EditText kod = FindViewById<EditText>(Resource.Id.edit_kod);
            EditText nralbumu = FindViewById<EditText>(Resource.Id.edit_nralbumu);
            kolko = FindViewById<ProgressBar>(Resource.Id.progressBarMain);
            Context mContex = this;
            zaloguj.Click += delegate 
            {
                int l;
                if (!((nralbumu.Text.Length == 6 | nralbumu.Text.Length == 5) & (Int32.TryParse(nralbumu.Text, out l))))
                {
                    Toast komunikat = Toast.MakeText(this, "Błędny numer albumu!!!", ToastLength.Long);
                    komunikat.Show();
                } 
                else if(kod.Text.Length == 0)
                {
                    Toast komunikat = Toast.MakeText(this, "Proszę zeskanować kod!!!", ToastLength.Long);
                    komunikat.Show();
                }
                else
                {
                    User.kodkreskowy = kod.Text;
                    User.nralbumu = nralbumu.Text;
                    mButton_zaloguj(mContex);
                }

            };
            
            Button Skanuj = FindViewById<Button>(Resource.Id.Skanuj);
            MobileBarcodeScanner.Initialize(Application);
            Skanuj.Click += async (sender, e) =>
            {
                var opt = new MobileBarcodeScanningOptions();
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                opt.DelayBetweenContinuousScans = 3000;
                scanner.TopText = "Utrzymuj kreskę na kodzie kreskowym\ntak aby był cały na ekranie\n(ok 10 com od ekranu)";
                scanner.BottomText = "Dotknij ekranu aby zlapac ostrość!";
                // scanner.ScanContinuously(opt, HandleScanResultContinuous);
                var result = await scanner.Scan();

                if (result != null)
                    kod.Text = result.Text;
                
            };
        }


        private void mButton_zaloguj(Context mContex)
        {
            //kolko.StartAnimation()
            OrderDTO orderDTO = new OrderDTO()
            {
                Name = "",
                SecondName = "",
                Faculty = "WMiI",
                Mode = "",
                Barcode = User.kodkreskowy,
                StudentNumber = User.nralbumu
            };
           
            try
            {
                REST.REST_Login(orderDTO);
            }
            catch
            {

                Toast komunikat = Toast.MakeText(this, "Problem z połączeniem internetowym!!!", ToastLength.Long);
                komunikat.Show();
            }
           
            if (User.orderdto != null)
            {
                Intent intent = new Intent(this, typeof(Panel_Menu));
                this.StartActivity(intent);
                this.Finish();
            }
        }
    }

}

