using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextualDevelopment.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = GetUrl();
            
            Post_003(url);
            
            System.Console.ReadLine();
        }

        static string GetUrl()
        {
            string host = "localhost";

            if(System.Diagnostics.Process.GetProcessesByName("fiddler").Any())
            {
                host = "ipv4.fiddler";
            }

            return string.Format("http://{0}:9200", host);
        }

        static void Post_003(string url)
        {
            // Elasticsearch.Net
            Console.WriteLine(" -- via Elasticsearch.Net -- ");
            var client = TextualDevelopment.Elasticsearch.Net.Post_003.SpecifyHostConnect(url);
            Console.WriteLine(TextualDevelopment.Elasticsearch.Net.Post_003.GetInfo(client));

            // NEST
            Console.WriteLine(" -- via NEST -- ");
            var nestClient = TextualDevelopment.NEST.Post_003.SpecifyHostConnect(url);
            Console.WriteLine(TextualDevelopment.NEST.Post_003.GetInfo(nestClient));
        }
    }
}
