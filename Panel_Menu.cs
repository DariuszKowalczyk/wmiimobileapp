using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util.Zip;

namespace projekt
{
    [Activity(Label = "Panel_Menu")]
    public class Panel_Menu : Activity
    {
        Button button_oferta;
        Button button_koszyk;
        Button button_Zmien_Dane;
        Button button_Strona;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Panel_Menu);

            button_oferta = FindViewById<Button>(Resource.Id.button_oferta);
            button_oferta.Click += mButton_oferta;

            button_koszyk = FindViewById<Button>(Resource.Id.button_koszyk);
            button_koszyk.Click += mButton_koszyk;

            button_Zmien_Dane = FindViewById<Button>(Resource.Id.Button_Zmien_dane);
            button_Zmien_Dane.Click += delegate
            {
                FragmentTransaction tranzakcja = FragmentManager.BeginTransaction();
                Zmien_dane dialog = new Zmien_dane();

                dialog.Show(tranzakcja, "Zmiana danych");
            };

            button_Strona = FindViewById<Button>(Resource.Id.Strona);
            button_Strona.Click += delegate {
                var uri = Android.Net.Uri.Parse("http://sklepkortowiadawmii.azurewebsites.net");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };
        
            // Create your application here
            REST.REST_GetProduct(this);
            REST.REST_GetKosz();
        }

        private void Button_Zmien_Dane_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            Zmien_dane dialog =  new Zmien_dane();
            dialog.Show(transaction, "Zmieñ dane");
            
        }

        private void mButton_oferta(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Oferta));
            this.StartActivity(intent);
        }
        private void mButton_koszyk(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Koszyk));
            this.StartActivity(intent);
        }
    }
}