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
    [Activity(Label = "Koszyk")]
    public class Koszyk : Activity
    {
        public static TextView text_suma;
        private ListView mListView;
 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Koszyk);
            Context context = this;
            mListView = FindViewById<ListView>(Resource.Id.kosz_listview);
            MyViewAdapter adapter = new MyViewAdapter(this, Kosz.lista, Resource.Layout.Koszyk);
            mListView.Adapter = adapter;

            #region suma
            Button zatwierc = FindViewById<Button>(Resource.Id.kosz_zatwierc);
            text_suma = FindViewById<TextView>(Resource.Id.kosz_cena);

            Set_Sum(text_suma);

            #endregion

            // Create your application here
        }
        public static void Del_produkt(Produkt produkt)
        {
            Kosz.suma -= produkt.Price;
            Kosz.lista.Remove(produkt);
            Set_Sum(text_suma);

        }
        public static void Set_Sum(TextView textview)
        {
            textview.Text = "Suma : " + Kosz.suma;
            textview.Invalidate();
        }

    }

    public static class Kosz
    {
        public static decimal suma;
        public static List<Produkt> lista = new List<Produkt>();
      //  public static Button przycisk = (Button)(Resource.Id.)
        public static void Add_produkt(Produkt produkt)
        {
            lista.Add(produkt);
            suma += produkt.Price;
            
        }
        
        public static bool czy_pusty()
        {
            if (lista.Count != 0)
                return true;
            else
                return false;
        }
        public static string return_name(int position)
        {
            return lista[position].Name;
        }
       
    }
}