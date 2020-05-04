using System;

using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoinMachine
{
    internal class HttpServer
    {
        //netsh http add urlacl url=http://+:4200/ user=Everyone
        public static HttpListener listener;

        public static string url = "http://+:4200/";
        public static int pageViews = 0;
        public static int requestCount = 0;

        public HttpServer()
        {
            _ = HandleIncomingConnections();
        }

        public void RunAsync()
        {
            // Task listenTask = HandleIncomingConnections();
            //listenTask.GetAwaiter().GetResult();
        }

        public static string pageData =
          "<!DOCTYPE>" +
          "<html>" +
          "<head>" +
          "<title>HttpListener Example</title>" +
          "</head>" +
          "<body>" +
          "<p>Page Views: {0}</p>" +
          "<form method=\"post\" action=\"shutdown\">" +
          "<input type=\"submit\" value=\"Shutdown\" {1}>" +
          "</form>" +
          "</body>" +
          "</html>";

        public async Task HandleIncomingConnections()
        {
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            bool runServer = true;
            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();
                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
                // Print out some info about the request
                //Console.WriteLine("Request #: {0}", ++requestCount);
                //Console.WriteLine(req.Url.ToString());
                //Console.WriteLine(req.HttpMethod);
                //Console.WriteLine(req.UserHostName);
                //Console.WriteLine(req.UserAgent);
                // Console.WriteLine();
                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }
                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;
                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;
                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
            listener.Close();
        }
    }
}