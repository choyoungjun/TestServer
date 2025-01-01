using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Windows.Forms;
//using Newtonsoft.Json;


namespace RestAPI_Server
{
    public class RestAPI_Server
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server Start.");
            test_server test = new test_server();
            test.serverInit();

            while (true)
            {

            }
        }
    }

    public class test_server
    {
        HttpListener httpListener = null;

        public void serverInit()
        {
            if (httpListener == null)
            {
                httpListener = new HttpListener();
                httpListener.Prefixes.Add(string.Format("http://+:8686/"));
                serverStart();
            }
        }

        private void serverStart()
        {
            if (!httpListener.IsListening)
            {
                httpListener.Start();
                //richTextBox1.Text = "Server is started";


                Task.Factory.StartNew(() =>
                {
                    while (httpListener != null)
                    {
                        HttpListenerContext context = this.httpListener.GetContext();

                        string rawurl = context.Request.RawUrl;
                        string httpmethod = context.Request.HttpMethod;

                        string text;
                        //result += string.Format("httpmethod = {0}\r\n", httpmethod);
                        //result += string.Format("url = {0}, ", rawurl);
                        var request = context.Request;
                        using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                        {
                            text = reader.ReadToEnd();
                        }

                        string result = "";
                        result += "{ " + text + "}";
                        Console.WriteLine("Post : " + result);

                        HttpListenerResponse response = context.Response;

                        response.ContentType = "application/json;charset=utf-8";
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(result);
                        response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();

                        context.Response.Close();
                    }
                });

            }
        }
    }
}






