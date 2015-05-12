using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using System;
using System.Text;

namespace TextualDevelopment.Elasticsearch.Net
{
    public static class Post_003
    {
        public static ElasticsearchClient SpecifyHostConnect(string url)
        {
            // simple connection
            var uri = new Uri(url);
            var config = new ConnectionConfiguration(uri);            
            return new ElasticsearchClient(config);
        }

        public static ElasticsearchClient ConnectionPoolConnect(string url)
        {
            // asks for nodes in the cluster,
            // then calls nodes round-robin, 
            // and provides failover support
            var uri = new Uri(url);
            var connectionPool = new SniffingConnectionPool(new[] { uri });
            var config = new ConnectionConfiguration(connectionPool);
            return new ElasticsearchClient(config);
        }

        public static string GetInfo(ElasticsearchClient client)
        {
            // http://localhost:9200?pretty
            var response = client.Info<string>();
            return response.Response;
        }
    }
}
