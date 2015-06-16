using Elasticsearch.Net.ConnectionPool;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using TextualDevelopment.Domain.RollingStone;

namespace TextualDevelopment.NEST
{
    public static class Post_006
    {
        public static void BulkIndex(string url)
        {
            // todo(rh): show success
            // todo(rh): show failure : client (200 response + 4xx on items with errors)
            // todo(rh): show failure : server (500 response) ?
            // todo(rh): fix album info -- correct details for 500 - 498
            // todo(rh): show id inference via attribute ?

            Bulk_Index_Basic(url);
            //Bulk_Index_DefaultIdx(url);
            //Bulk_Index_Iterate(url);
        }

        public static void Bulk_Index_Basic(string url)
        {
            var uri = new Uri(url);
            var config = new ConnectionSettings(uri);
            var client = new ElasticClient(config);

            Album a1 = new Album() { Title = "Album 500", Rank = 500 };
            Album a2 = new Album() { Title = "Album 499", Rank = 499 };
            Album a3 = new Album() { Title = "Album 498", Rank = 498 };

            IBulkResponse response =  client.Bulk(br => br
                .Index<Album>(bid => bid
                    .Index("rolling-stone-500")
                    .Id(a1.Rank)
                    .Document(a1)
                )
                .Create<Album>(bid => bid
                    .Index("rolling-stone-500")
                    .Id(a2.Rank)
                    .Document(a2)
                )
                .Index<Album>(bid => bid
                    .Index("rolling-stone-500")
                    .Id(a3.Rank)
                    .Document(a3)
                )
                // ... etc ...
            );

            ExamineResponse(response);

        }

        public static void Bulk_Index_DefaultIdx(string url)
        {
            var uri = new Uri(url);
            var config = new ConnectionSettings(uri);
            config.SetDefaultIndex("rolling-stone-500");
            var client = new ElasticClient(config);

            Album a1 = new Album();
            Album a2 = new Album();
            Album a3 = new Album();

            client.Bulk(br => br
                .Index<Album>(bid => bid                    
                    .Id(500)
                    .Document(a1)
                )
                .Create<Album>(bid => bid
                    .Id(499)
                    .Document(a2)
                )
                .Index<Album>(bid => bid
                    .Id(498)
                    .Document(a3)
                )
                // ... etc ...
            );
        }

        public static void Bulk_Index_Iterate(string url)
        {
            var uri = new Uri(url);
            var config = new ConnectionSettings(uri);
            config.SetDefaultIndex("rolling-stone-500");
            var client = new ElasticClient(config);

            List<Album> albums = new List<Album> {
                new Album(),
                new Album(),
                new Album(),
            };
            
            var bulkDescriptor = new BulkDescriptor();
            foreach (var album in albums)
            {
                bulkDescriptor.Index<Album>(bid => bid
                    .Id(album.Rank)
                    .Document(album)
                );
            }

            var result = client.Bulk(bulkDescriptor);
        }

        private static void ExamineResponse(IBulkResponse response)
        {
            Console.WriteLine("Errors: {0}", response.Errors);

            //Console.WriteLine("Items:");
            //foreach(BulkOperationResponseItem item in response.Items)
            //{
            //    Console.WriteLine("\tIndex: {0}, Type: {1}, Id: {2}, Error: {3}",
            //        item.Index, item.Type, item.Id, item.Error);
            //}

            Console.WriteLine("ItemsWithErrors:");
            foreach (BulkOperationResponseItem item in response.ItemsWithErrors)
            {
                Console.WriteLine("\tIndex: {0}, Type: {1}, Id: {2}, Error: {3}",
                    item.Index, item.Type, item.Id, item.Error);
            }
            
            Console.WriteLine();
        }
    }
}
