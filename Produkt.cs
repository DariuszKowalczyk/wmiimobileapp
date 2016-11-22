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
using Android.Graphics;
using System.Net;
using Android.Transitions;

namespace projekt
{
    // produkt do resta
    /*
    public class Produkt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
    }
    */
    // test produkt 
    public class Produkt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
   
        public Produkt(string nazwa, decimal cena, string opis, string zdj)
        {
            this.Name = nazwa;
            this.Price = cena;
            this.Description = opis;
            this.PictureUrl = zdj;
        }
    }
    
    class MyFirstViewAdapter : BaseAdapter<Produkt>
    {
        public List<Produkt> mItems;
        private Context mContext;
        public View row;
        int MResource;

        public MyFirstViewAdapter(Context context, List<Produkt> items, int resourc)
        {
            mItems = items;
            mContext = context;
            MResource = resourc;
        }
        public override int Count
        {
            get { return mItems.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Produkt this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            row = convertView;            
            if (MResource == Resource.Layout.Oferta)
            {
                if (row == null)
                {
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListProdukt_Row, null, false);
                }
                R_l_Oferta(position);
                row.Click += delegate {
                    przycisk_odkryj();                
                    row.FindViewById<TextView>(Resource.Id.txtOpis).Visibility = ViewStates.Invisible;
                };
                Button przycisk = row.FindViewById<Button>(Resource.Id.Dodaj_do_koszyka);
                przycisk.Click += delegate{ Okienko(position); };
                row.Click += delegate { przycisk.Visibility = ViewStates.Visible; };
            }
            else if (MResource == Resource.Layout.Koszyk)
            {
                if (row == null)
                {
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Koszyk_Row, null, false);
                }
                R_l_Koszyk(position);

            }

            return row;
            
            
        }
        protected void Set_Text(object text, TextView textview)
        {
            textview.Text = Convert.ToString(text); 
        }
        protected void Set_Image(string url, ImageView imageview)
        {
            if(url != null)
            {
                Bitmap imageBitmap = null;
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);  
                        imageview.SetImageBitmap(imageBitmap);
                    }
                }
            }
        }
        protected void Okienko(int position)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(mContext);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Czy chcesz doda� produkt do koszyka?");
            alertDialog.SetMessage(mItems[position].Name + "\nCena: " + mItems[position].Price);
            //alertDialog.SetButton2("Ok", (s, ev) => { Toast.MakeText(mContext, "dziala", ToastLength.Long); });
            alertDialog.SetButton2("Tak", (s, ev) => { Kosz.lista.Add(mItems[position]); });
            alertDialog.SetButton("Nie", (s, ev) => { alertDialog.Cancel(); });
            alertDialog.Show();
        }
        protected void R_l_Oferta(int position)
        {
            Set_Text(mItems[position].Name, row.FindViewById<TextView>(Resource.Id.txtNazwa));
            Set_Text(mItems[position].Price, row.FindViewById<TextView>(Resource.Id.txtCena));
            Set_Text(mItems[position].Description, row.FindViewById<TextView>(Resource.Id.txtOpis));
            Set_Text(mItems[position].PictureUrl, row.FindViewById<TextView>(Resource.Id.txtDokladnosc));
            Set_Image(mItems[position].PictureUrl, row.FindViewById<ImageView>(Resource.Id.ImageProdukt));
        }
        protected void R_l_Koszyk(int position)
        {
            Set_Text(mItems[position].Name, row.FindViewById<TextView>(Resource.Id.kosz_name));
            Set_Text(mItems[position].Price, row.FindViewById<TextView>(Resource.Id.kosz_cena));
            Set_Text(mItems[position].Description, row.FindViewById<TextView>(Resource.Id.kosz_opis));
            Set_Image(mItems[position].PictureUrl, row.FindViewById<ImageView>(Resource.Id.kosz_obraz));
        }
        public void przycisk_odkryj()
        {
            
            //przycisk.Visibility = ViewStates.Visible;
        }
    }
}