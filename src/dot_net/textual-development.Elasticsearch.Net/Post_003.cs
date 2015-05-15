using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using System;
using System.Text;

namespace TextualDevelopment.Elasticsearch.Net
{
    public static class Post_003
    {
        public static ElasticsearchClient SpecifyHostConnect()
        {
            //------------------------
            var uri = new Uri("http://localhost:9200");
            var config = new ConnectionConfiguration(uri);            
            var client = new ElasticsearchClient(config);
            //------------------------

            return client;
        }

        public static void ShowInfo(ElasticsearchClient client)
        {
            //------------------------
            var response = client.Info<string>();
            Console.WriteLine(response.Response);
            //------------------------
        }
    }
}
