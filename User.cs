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
using projekt.DTO;

namespace projekt
{
    public static class User
    {
       
        public static string nralbumu { get; set; }
        public static string kodkreskowy { get; set; }
        public static OrderDTO orderdto { get; set; }
   
       
       
        static public string Get_NrAlbumu()
        {
            return nralbumu;
        }
        static public string Get_KodKreskowy()
        {
            return kodkreskowy;
        }
        static public void Set_NrAlbumu(string Nralbumu)
        {
            nralbumu = Nralbumu;
        }
        static public void Set_KodKreskowy(string kod)
        {
            kodkreskowy = kod;
        }


    }
}