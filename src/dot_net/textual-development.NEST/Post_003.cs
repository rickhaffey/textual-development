using Elasticsearch.Net.ConnectionPool;
using Nest;
using System;
using System.Text;

namespace TextualDevelopment.NEST
{
    public static class Post_003
    {
        public static ElasticClient SpecifyHostConnect(string url)
        {
            // simple connection
            var uri = new Uri(url);
            var config = new ConnectionSettings(uri);
            return new ElasticClient(config);
        }

        public static ElasticClient ConnectionPoolConnect(string url)
        {
            // asks for nodes in the cluster,
            // then calls nodes round-robin, 
            // and provides failover support
            var uri = new Uri(url);
            var connectionPool = new SniffingConnectionPool(new[] { uri });
            var config = new ConnectionSettings(connectionPool);
            return new ElasticClient(config);
        }

        public static string GetInfoX(ElasticClient client)
        {
            // http://localhost:9200?pretty
            var response = client.RootNodeInfo();
          
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}{2}", "Status", response.Status, Environment.NewLine);
            sb.AppendFormat("{0}: {1}{2}", "Name", response.Name, Environment.NewLine);
            sb.AppendFormat("{0}: {1}{2}", "Version Number", response.Version.Number, Environment.NewLine);
            sb.AppendFormat("{0}: {1}{2}", "Version Is Snapshot Build", response.Version.IsSnapShotBuild, Environment.NewLine);
            sb.AppendFormat("{0}: {1}{2}", "Lucene Version", response.Version.LuceneVersion, Environment.NewLine);
            sb.AppendFormat("{0}: {1}{2}", "Tagline", response.Tagline, Environment.NewLine);            

            return sb.ToString();
        }

        public static void GetInfo(ElasticClient client)
        {
            var response = client.RootNodeInfo();
            Console.WriteLine("Status: " + response.Status);
            Console.WriteLine("Name: " + response.Name);
            Console.WriteLine("Version Number: " + response.Version.Number);
            Console.WriteLine("Version Is Snapshot Build: " + response.Version.IsSnapShotBuild);
            Console.WriteLine("Lucene Version: " + response.Version.LuceneVersion);
            Console.WriteLine("Tagline: " + response.Tagline);


            var s = client.Raw.Info<string>();
        }





    }
}
