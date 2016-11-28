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

namespace projekt
{
    [Activity(Label = "projekt", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Button zaloguj = FindViewById<Button>(Resource.Id.zaloguj);
            EditText kod = FindViewById<EditText>(Resource.Id.editKod);
            zaloguj.Click += mButton_zaloguj;
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


        void mButton_zaloguj(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Panel_Menu));
            this.StartActivity(intent);
            this.Finish();
        }
    }
}

