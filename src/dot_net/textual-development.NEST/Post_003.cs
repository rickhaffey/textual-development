using Elasticsearch.Net.ConnectionPool;
using Nest;
using System;
using System.Text;

namespace TextualDevelopment.NEST
{
    public static class Post_003
    {
        public static ElasticClient SpecifyHostConnect()
        {                 
            //------------------------
            var uri = new Uri("http://localhost:9200");
            var config = new ConnectionSettings(uri);
            var client = new ElasticClient(config);
            //------------------------
            
            return client;
        }

        public static void ShowInfo(ElasticClient client)
        {
            //------------------------
            var response = client.RootNodeInfo();
            Console.WriteLine("Status: " + response.Status);
            Console.WriteLine("Name: " + response.Name);
            Console.WriteLine("Version Number: " + response.Version.Number);
            Console.WriteLine("Version Is Snapshot Build: " + response.Version.IsSnapShotBuild);
            Console.WriteLine("Lucene Version: " + response.Version.LuceneVersion);
            Console.WriteLine("Tagline: " + response.Tagline);
            //------------------------            
        }


        public static void TypedVsRaw(ElasticClient client)
        {
            //------------------------
            var response1 = client.RootNodeInfo(); // returns typed IRootInfoReponse
            var response2 = client.Raw.Info<string>(); // returns raw JSON response
            //------------------------
        }
    }
}
