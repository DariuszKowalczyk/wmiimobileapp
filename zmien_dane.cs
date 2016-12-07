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

namespace projekt
{
    public class Zmien_dane : DialogFragment
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Zmien_dane, container, false);

            
            EditText zm_imie = view.FindViewById<EditText>(Resource.Id.zm_imie);
            zm_imie.Text = (string)User.orderdto.Name;
            EditText zm_nazwisko = view.FindViewById<EditText>(Resource.Id.zm_nazwisko);
            zm_nazwisko.Text = (string)User.orderdto.SecondName;
            EditText zm_nralbumu = view.FindViewById<EditText>(Resource.Id.zm_nralbumu);
            zm_nralbumu.Text = (string)User.Get_NrAlbumu();
            TextView zm_text_kod = view.FindViewById<TextView>(Resource.Id.zm_text_kod);
            zm_text_kod.Text = (string)User.Get_KodKreskowy();

            Button Zapisz = view.FindViewById<Button>(Resource.Id.zm_zapisz);
            Button Analuj = view.FindViewById<Button>(Resource.Id.zm_anuluj);
            Zapisz.Click += delegate {
                try {
                    REST.REST_ZmienDane(zm_imie.Text, zm_nazwisko.Text);
                    ustaw(zm_imie.Text, zm_nazwisko.Text);
                    Toast komunikat = Toast.MakeText(view.Context, "Zmiany zosta³y zapisane.", ToastLength.Long);
                    komunikat.Show();
                }
                catch
                {
                    Toast komunikat = Toast.MakeText(view.Context, "Problem z internetem, spróbuj póŸniej.", ToastLength.Long);
                    komunikat.Show();
                }
                finally { this.Dismiss(); } };



            Analuj.Click += delegate { this.Dismiss(); };

            return view;
           

         }
        private void ustaw(string imie, string nazwisko)
        {
            User.orderdto.Name = imie;
            User.orderdto.SecondName = nazwisko;
        }
    }
}