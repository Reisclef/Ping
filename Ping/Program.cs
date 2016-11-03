using System;
using System.Net;
using System.Threading;
using System.IO;

namespace Ping {
    class Program {
        static void Main(string[] args) {
            if (args.Length != 2) {
                Console.WriteLine("Usage: URL_to_ping log_file");
            }
            else {
                string path = args[1];
                for (int i = 0; i < 450; i++) {
                    //Prepare the log message, and the request
                    string text = Environment.NewLine + DateTime.Now;
                    WebRequest request = WebRequest.Create(args[0]);

                    //See if the response was OK
                    try {
                        using (WebResponse response = request.GetResponse()) {
                            text = text + " : Success";
                        }
                    }
                    //If not, catch the exception, and state what the status code returned was
                    catch (WebException ex) {
                        using (WebResponse response = ex.Response) {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;
                            text = text + " : Failed. Error: " + (string)httpResponse.StatusCode.ToString();
                        }
                    }
                    //Write this to file, and to the console.
                    File.AppendAllText(path, text);
                    Console.WriteLine(text);

                    //Wait 1 minute before going again
                    Thread.Sleep(60000);
                }
            }
            
        }
    }
}
