using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DiceBagApp.Services
{
    public class RestService : IRestService
    {
        private String UrlServerRest { get;  }

        public RestService()
        {
            UrlServerRest = "http://localhost:8000/";
        }



        public String GetUsers()
        {

            var urlApi = String.Format("{0}/api/Users", UrlServerRest);
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(urlApi);
            string result;

            using (var response = webrequest.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
                return result;
            }
        }


    }
}
