using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Net;
using System;
using System.Collections.Generic;
using System.Net;

namespace projekt
{
    class MyViewAdapter : BaseAdapter<Produkt>
    {
        public List<Produkt> mItems;
        public Context mContext;
        public View row;
        int MResource;

        public MyViewAdapter(Context context, List<Produkt> items, int resourc)
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
            row = null;
            if (MResource == Resource.Layout.Oferta)
            {
                if (row == null)
                {
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListProdukt_Row, parent, false);
                }
                R_l_Oferta(position);
                Button przycisk = row.FindViewById<Button>(Resource.Id.Dodaj_do_koszyka);
                przycisk.Visibility = ViewStates.Gone;
                przycisk.Click += delegate { Okienko_Dodaj(position); };
                row.Click += delegate { if (przycisk.Visibility == ViewStates.Gone){ przycisk.Visibility = ViewStates.Visible; } else { przycisk.Visibility = ViewStates.Gone; } };

            }
            else if (MResource == Resource.Layout.Koszyk)
            {
                if (row == null)
                {
                    row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Koszyk_Row, null, false);
                    R_l_Koszyk(position);
                    Button przycisk = row.FindViewById<Button>(Resource.Id.kosz_del);

                    przycisk.Click += delegate { Okienko_Usun(position); };
                    
                }
            }

            return row;

        }



        protected void Set_Text(object text, TextView textview)
        {
            textview.Text = Convert.ToString(text);
        }
        protected void Set_Image(string url, ImageView imageview)
        {
            if (url != null)
            {
                Bitmap imageBitmap = null;
                try
                {
                    using (var webClient = new WebClient())
                    {
                        var imageBytes = webClient.DownloadData(url);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);                        
                            imageview.SetImageBitmap(imageBitmap);
                        }
                    }
                }catch { }   
                
            }
        }


        protected void Okienko_Dodaj(int position)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(mContext);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Czy chcesz dodaæ produkt do koszyka?");
            alertDialog.SetMessage(mItems[position].Name + "\nCena: " + mItems[position].Price);
            alertDialog.SetButton2("Tak", (s, ev) => { Kosz.Add_produkt(mItems[position]); alertDialog.Cancel(); });
            alertDialog.SetButton("Nie", (s, ev) => { alertDialog.Cancel(); });
            alertDialog.Show();
        }
        protected void Okienko_Usun(int position)
        {
            try
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(mContext);
                AlertDialog alertDialog = builder.Create();
                alertDialog.SetTitle("Czy chcesz usun¹æ produkt z koszyka?");
                alertDialog.SetMessage(mItems[position].Name + "\nCena: " + mItems[position].Price);
                alertDialog.SetButton2("Tak", (s, ev) => { Koszyk.Del_produkt(mItems[position]); NotifyDataSetChanged(); alertDialog.Cancel(); });
                alertDialog.SetButton("Nie", (s, ev) => { alertDialog.Cancel(); });
                alertDialog.Show();
            }
            catch
            {
                NotifyDataSetChanged();
            }
        }
        protected void R_l_Oferta(int position)
        {
            Set_Text(mItems[position].Name, row.FindViewById<TextView>(Resource.Id.txtNazwa));
            Set_Text(mItems[position].Price, row.FindViewById<TextView>(Resource.Id.txtCena));
            Set_Text(mItems[position].Description, row.FindViewById<TextView>(Resource.Id.txtOpis));
            Set_Image(mItems[position].PictureUrl, row.FindViewById<ImageView>(Resource.Id.ImageProdukt));
        }
        protected void R_l_Koszyk(int position)
        {
            Set_Text(mItems[position].Name, row.FindViewById<TextView>(Resource.Id.kosz_name));
            Set_Text(mItems[position].Price, row.FindViewById<TextView>(Resource.Id.kosz_cena));
            Set_Text(mItems[position].Description, row.FindViewById<TextView>(Resource.Id.kosz_opis));
            Set_Image(mItems[position].PictureUrl, row.FindViewById<ImageView>(Resource.Id.kosz_obraz));
        }



    }
}
