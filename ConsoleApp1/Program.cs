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

            while(true)
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

                        string result = "";

                        result += string.Format("httpmethod = {0}\r\n", httpmethod);
                        result += string.Format("rawurl = {0}\r\n", rawurl);

                        // if (richTextBox1.InvokeRequired)
                        //     richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.Text = result; }));
                        // else
                        //     richTextBox1.Text = result;

                        Console.WriteLine("Post : " + result);

                        context.Response.Close();

                    }
                });

            }
        }
    }
}






