using System;
using System.Net;
using System.Threading;
using System.IO;

namespace Ping {
    class Program {
        static string location = "https://google.com";
        static string path = Directory.GetCurrentDirectory() + "\\ping.log";
        static int repetitions = 1;
        static void Main(string[] args) {
            //Based on the number of arguments, set the variables from default
            switch (args.Length) {
                case 0:
                    Console.WriteLine("Usage: URL_to_ping log_file repetitions");
                    break;
                case 1:
                    Console.WriteLine("Usage: URL_to_ping log_file repetitions");
                    location = args[0];
                    break;
                case 2:
                    Console.WriteLine("Usage: URL_to_ping log_file repetitions");
                    location = args[0];
                    path = args[1];
                    break;
                case 3:
                    location = args[0];
                    path = args[1];
                    repetitions = Convert.ToInt32(args[2]);
                    break;
            }

            //Write a message to begin the file
            WriteAndLog(Environment.NewLine + $"Pinging {location} and logging to {path}");

            //Run this every minute for specified number of minutes (default 1)
            for (int i = 0; i <= repetitions; i++) {

                //If this is not the first run...
                if (i != 0) {
                    //Then wait 1 minute before next ping
                    if (i != repetitions) {
                        Thread.Sleep(1000 * 60);
                    }
                    //But if this is the last run, stop now
                    else {
                        break;
                    }
                }

                //Prepare the log message, and the request
                string text = Environment.NewLine + DateTime.Now;
                WebRequest request = WebRequest.Create(location);

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
                        text = text + " : Failed. Error: " + httpResponse.StatusCode.ToString();
                    }
                }

                //Regardless of what occurred, state what happened
                WriteAndLog(text);
            }
        }
        //Write a specified message to specified file, and to the console.
        static void WriteAndLog(string message) {
            File.AppendAllText(path, message);
            Console.WriteLine(message);
        }
    }
}
