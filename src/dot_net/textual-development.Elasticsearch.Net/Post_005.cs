using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using System;
using System.Linq;
using System.Text;
using TextualDevelopment.Domain.RollingStone;

namespace TextualDevelopment.Elasticsearch.Net
{    
    public static class Post_005
    {
        public static void GetAlbum(string url)
        {
            var uri = new Uri(url);
            var config = new ConnectionConfiguration(uri);
            config.ExposeRawResponse(true);
            var client = new ElasticsearchClient(config);

            // 'dynamically'-typed
            Console.WriteLine(" --- 'dynamically'-typed --- ");

			ElasticsearchResponse<DynamicDictionary> response = client.Get("rolling-stone-500", "album", "59");           
            Console.WriteLine("_index: " + response.Response["_index"]);
            Console.WriteLine("_type: " + response.Response["_type"]);
            Console.WriteLine("_id: " + response.Response["_id"]);
            Console.WriteLine("_version: " + response.Response["_version"]);
            Console.WriteLine("found: " + response.Response["found"]);
			Console.WriteLine("_source: " + response.Response ["_source"]);
            
            // strongly-typed
            Console.WriteLine(" --- strongly-typed --- ");
			ElasticsearchResponse<Album> response2 = client.Get<Album>("rolling-stone-500", "album", "59");

			// NOTE: all the below is returning as null or empty -- needs more investigation as to why
			Console.WriteLine();
			Console.WriteLine("SOURCE: ");
			Console.WriteLine("Title: {0}", response2.Response.Title);
			Console.WriteLine("Url: {0}", response2.Response.Url);
			Console.WriteLine("Artist: {0}", response2.Response.Artist);
			Console.WriteLine("Rank: {0}", response2.Response.Rank);
			Console.WriteLine("Label: {0}", response2.Response.Label);
			Console.WriteLine("Year: {0}", response2.Response.Year);
			Console.WriteLine("Summary: {0}", response2.Response.Summary);
			Console.WriteLine("ImageUrl: {0}", response2.Response.ImageUrl);			          

            // show raw response (requires config on connection)
            Console.WriteLine(" --- raw response --- ");
            Console.WriteLine(Encoding.UTF8.GetString(response2.ResponseRaw));
        }    
    }
}
