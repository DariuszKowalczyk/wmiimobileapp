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
    
    public class Produkt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
    }
    
    // test produkt 
    /*
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
    */
}