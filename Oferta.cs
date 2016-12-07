using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;

namespace projekt
{
    [Activity(Label = "Oferta")]
    public class Oferta : Activity
    {
        //private List<Produkt> mItems;
        private ListView mListView;
       // private List<Produkt> mItems;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Oferta);
            Context context = this;

            #region Rest i ladowanie listy
            mListView = FindViewById<ListView>(Resource.Id.listaProdukt);
            #region /************ REST ****************

            REST.REST_GetProduct(this);
            
            #endregion//******************************

           
            MyViewAdapter adapter = new MyViewAdapter(this, REST.listaP, Resource.Layout.Oferta);
            
            mListView.Adapter = adapter;
            #endregion


            // Create your application here
        }        
    }
}