using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using System;
using System.Linq;
using System.Text;
using TextualDevelopment.Domain.RollingStone;

namespace TextualDevelopment.Elasticsearch.Net
{    
    public static class Post_004
    {
        public static ElasticsearchResponse<DynamicDictionary> IndexAlbum(string url)
        {
            var uri = new Uri(url);
            var config = new ConnectionConfiguration(uri);
            config.ExposeRawResponse(true);
            var client = new ElasticsearchClient(config);

            // 'dynamically'-typed
            Console.WriteLine(" --- 'dynamically'-typed --- ");
            ElasticsearchResponse<DynamicDictionary> response = client.Index("rolling-stone-500", "album", "1", Album.ChronicleV1);
            Console.WriteLine("_index: " + response.Response["_index"]);
            Console.WriteLine("_type: " + response.Response["_type"]);
            Console.WriteLine("_id: " + response.Response["_id"]);
            Console.WriteLine("_version: " + response.Response["_version"]);
            Console.WriteLine("created: " + response.Response["created"]);
            
            // strongly-typed
            Console.WriteLine(" --- strongly-typed --- ");
            ElasticsearchResponse<Album> response2 = client.Index<Album>("rolling-stone-500", "album", "2", Album.ChronicleV1);
            Console.WriteLine("_index: " + response2.Response._index);
            Console.WriteLine("_type: " + response2.Response._type);
            Console.WriteLine("_id: " + response2.Response._id);
            Console.WriteLine("_version: " + response2.Response._version);            
            // Console.WriteLine("created: " + response2.Response.created);  // removed this because it's getting indexed as well -- only want it to pick up the response

            // show raw response (requires config on connection)
            Console.WriteLine(" --- raw response --- ");
            Console.WriteLine(Encoding.UTF8.GetString(response2.ResponseRaw));

            return response;
        }    
    }
}
