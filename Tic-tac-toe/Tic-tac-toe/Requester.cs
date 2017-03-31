using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Tic_tac_toe
{
    public class Requester
    {
        virtual public string GetRequest(string uri) {

            // адрес метода для получения данных
            Uri target = new Uri("http://localhost:58108/Home/GetGameInfo");
            WebRequest request = WebRequest.Create(target);

            request.Method = "GET";
            request.ContentType = "application/json";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        virtual public void PostRequest(string uri, byte[] data) {

            // адрес метода для сохранения данных
            Uri target = new Uri(uri);
            WebRequest request = WebRequest.Create(target);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(data, 0, data.Length);
            }            
        }
    }
}
