using Elasticsearch.Net.ConnectionPool;
using Nest;
using System;
using System.Text;
using TextualDevelopment.Domain.RollingStone;

namespace TextualDevelopment.NEST
{    
    public static class Post_004
    {
        public static IIndexResponse IndexAlbum(string url)
        {            
            var uri = new Uri(url);
            var config = new ConnectionSettings(uri);
            config.ExposeRawResponse(true);

            var client = new ElasticClient(config);            
            Album album = Album.ChronicleV1;

            // approach 1
            Index_IIndexRequest(client, album);

            // approach 2
            Index_IndexDescriptorFunc(client, album);

            // approach 3
            IIndexResponse response = Index_InlineFunc(client, album);

            ExamineResponse(response);

            return response;
        }

        private static void ExamineResponse(IIndexResponse response)
        {
            Console.WriteLine("_index: {0}", response.Index);
            Console.WriteLine("_type: {0}", response.Type);
            Console.WriteLine("_id: {0}", response.Id);
            Console.WriteLine("_version: {0}", response.Version);
            Console.WriteLine("created: {0}", response.Created);

            Console.WriteLine();
            Console.WriteLine("REQ Body: ");
            Console.WriteLine(Encoding.UTF8.GetString(response.ConnectionStatus.Request));
            Console.WriteLine();

            Console.WriteLine("RESP Body: ");
            Console.WriteLine(Encoding.UTF8.GetString(response.ConnectionStatus.ResponseRaw));
            Console.WriteLine();
            
            //Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings() {
            //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //};
            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response, settings));
        }
        
        private static void Index_IIndexRequest(ElasticClient client, Album album)
        {
            album.Rank = 3;     // establish our ID
           
            IIndexRequest<Album> req = new IndexRequest<Album>(album);
            req.Index = "rolling-stone-500";
            //req.Id = "59";
            
            IIndexResponse response1 = client.Index(req);
        }

        private static void Index_IndexDescriptorFunc(ElasticClient client, Album album)
        {
            album.Rank = 4;     // establish our ID

            Func<IndexDescriptor<Album>, IndexDescriptor<Album>> fun = (a) =>
            {
                return a
                    .Index("rolling-stone-500")
                    //.Id(4)
                    ;
            };
            
            IIndexResponse response2 = client.Index(album, fun);
        }
        
        private static IIndexResponse Index_InlineFunc(ElasticClient client, Album album)
        {
            album.Rank = 5;     // establish our ID

            IIndexResponse response = client.Index(album, a => a
                .Index("rolling-stone-500")
                //.Id(5)
            );            

            return response;
        }
    }
}
