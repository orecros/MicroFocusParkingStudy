using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IDOLFilePost {
    class Program {

        static string address = "http://localhost:14000/";
        static string configName = "Spot0Classifier";

        private static HttpClient client;

        static async Task Main(string[] args) {
            
            client = new HttpClient();

            List<Task> Tasks = new List<Task>();

            // for each file we dropped on the .exe, we want to make a new html request to the server
            foreach (string s in args) {
                Tasks.Add(PostFileAsync(s));
            }

            // wait for all our tasks to complete before returning
            foreach(Task t in Tasks) {
                await t;
            }
        }

        static async Task PostFileAsync(string filename) {

            //create an HTTP request with our config for this filename
            Dictionary<string,string> values = new Dictionary<string, string> {
                { "action=", "process" },
                { "&configname=", configName },
                { "&source=", filename}
            };
            string str = address;
            foreach(KeyValuePair<string,string> kv in values) {
                str += kv.Key + kv.Value;
            }
            

            //send the GET request
            string response;
            try {
                response = await client.GetStringAsync(str);
            }
            catch (HttpRequestException e) {
                response = e.ToString();
            }
        }
    }
}
