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
using RestSharp;
using projekt.DTO;
using System.Net;
using System.Net.Mail;

namespace projekt
{
    public static class REST
    {
        
        public static List<Produkt> listaP = new List<Produkt>();
        public static void REST_GetProduct(Context context)
        {
            REST_PGetProduct(context);
        }
        public static void REST_AddProduct(Produkt product)
        {
            REST_PAddProduct(product);
        }
        public static void REST_Login(OrderDTO orderDTO)
        {
            REST_PLogin(orderDTO);
        }
        public static void REST_Problem(Context context)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Problem z po³¹czeniem!!!");
            alertDialog.SetMessage("Problem z po³¹czeniem internetowym.");
            alertDialog.SetButton2("Ok", (s, ev) => { Toast.MakeText(context, "dziala", ToastLength.Long); });
            alertDialog.Show();
        }
        public static void REST_GetKosz()
        {
            REST_PGetKosz();
        }
        public static void REST_ZmienDane(string imie, string nazwisko)
        {
            REST_PZmienDane(imie, nazwisko);
        }
        public static void REST_DelPKosz(Context context, Produkt produkt)
        {
            REST_PDelPKosz(context, produkt);
        }

        private static void REST_PDelPKosz(Context context, Produkt produkt)
        {
            var client = new RestClient("http://sklepkortowiadawmii.azurewebsites.net");
            var request = new RestRequest("api/Orders/" + User.orderdto.Id, Method.DELETE);
            int numerZ = 0;
            foreach (var x in User.orderdto.Details)
            {
                if (x.ProductId == produkt.Id)
                {
                    numerZ = x.Number;
                    break;
                }

            }
            request.AddParameter("Number", numerZ);
            try
            {
                IRestResponse response = client.Execute(request);
                Koszyk.Del_produkt(produkt);
                Toast komunikat = Toast.MakeText(context, "Produkt usuniêto z koszyka.", ToastLength.Long);
                komunikat.Show();
            }
            catch
            {
                REST_Problem(context);
            }

        }
        private static void REST_PZmienDane(string imie, string nazwisko)
        {
            var client = new RestClient("http://sklepkortowiadawmii.azurewebsites.net");
            var request = new RestRequest("api/Orders/" + User.orderdto.Id, Method.PUT);
           
            OrderDTO orderDTO = new OrderDTO()
            {
                Id = User.orderdto.Id,
                Paid = User.orderdto.Paid,
                Received = User.orderdto.Received,
                Name = imie,
                SecondName = nazwisko,
                StudentNumber = User.orderdto.StudentNumber,
                Barcode = User.orderdto.Barcode,
                Faculty = User.orderdto.Faculty,
                Mode = User.orderdto.Mode
            };
            request.AddJsonBody(orderDTO);
            IRestResponse response = client.Execute(request);

        }
        private static void REST_PGetKosz()
        {
            Kosz.lista.Clear();
            foreach (var x in User.orderdto.Details)
            {
                Kosz.lista.Add(REST.listaP.Where(p => p.Id == x.ProductId).Single());
            }
            
        }    
        private static void REST_PLogin(OrderDTO orderDTO)
        {
            
            var client = new RestClient("http://sklepkortowiadawmii.azurewebsites.net");
            var request = new RestRequest("api/Orders/" + orderDTO.Barcode, Method.GET);
            var response = client.Get(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                request = new RestRequest("api/Orders", Method.POST);
                request.AddJsonBody(orderDTO);
                response = client.Execute(request);
            }
            orderDTO = SimpleJson.DeserializeObject<OrderDTO>(response.Content);
            User.orderdto = orderDTO;
          

        }
        private static void REST_PGetProduct(Context context)
        {
            var client = new RestClient("http://sklepkortowiadawmii.azurewebsites.net");
            var request = new RestRequest("api/Products", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            try
            {
                List<Produkt> listProdukt = SimpleJson.DeserializeObject<List<Produkt>>(content);
                listaP.Clear();
                listaP.AddRange(listProdukt);
            }
            catch
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(context);
                AlertDialog alertDialog = builder.Create();
                alertDialog.SetTitle("Problem z po³¹czeniem!!!");
                alertDialog.SetMessage("Problem z po³¹czeniem internetowym.");
                alertDialog.SetButton2("Ok", (s, ev) => { Toast.MakeText(context, "dziala", ToastLength.Long); });
                alertDialog.Show();
            }
        }
        private static void REST_PAddProduct(Produkt product)
        {
            var client = new RestClient("http://sklepkortowiadawmii.azurewebsites.net");
            var request = new RestRequest("api/Orders/" + User.orderdto.Id, Method.POST);
            OrderDetailDTO orderdetail = new OrderDetailDTO()
            {
                ProductId = product.Id,
                Quantity = 1,

            };
            request.AddJsonBody(orderdetail);
            IRestResponse response = client.Execute(request);
            User.orderdto = SimpleJson.DeserializeObject<OrderDTO>(response.Content);
            Kosz.Add_produkt(product);
            
        }

       

    }
}