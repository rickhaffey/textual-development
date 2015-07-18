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

            Bulk_Index_Error(url);
			//Bulk_Index_Basic(url);
			//Bulk_Index_DefaultIdx(url);
			//Bulk_Index_Iterate(url);
		}

        //public static void Bulk_Index_BulkRequest(string url)
        //{
        //    var uri = new Uri (url);
        //    var config = new ConnectionSettings (uri);
        //    var client = new ElasticClient (config);

        //    Album a1 = new Album () { Title = "Album 500", Rank = 500 };
        //    Album a2 = new Album () { Title = "Album 499", Rank = 499 };
        //    Album a3 = new Album () { Title = "Album 498", Rank = 498 };

        //    BulkRequest request = new BulkRequest { 
        //        Index = "rolling-stone-500",
        //        Type = "album",
        //        Operations = new List<IBulkOperation> {
        //            new BulkIndexOperation<Album> {
        //                Document = a1,
        //                Id = a1.Rank						
        //            },
        //            new BulkIndexOperation<Album> {
        //                Document = a2,
        //                Id = a2.Rank						
        //            },
        //            new BulkIndexOperation<Album> {
        //                Document = a3,
        //                Id = a3.Rank						
        //            }
        //        }
        //    };
        //}

		public static void Bulk_Index_Basic(string url)
		{
			var uri = new Uri (url);
			var config = new ConnectionSettings (uri);
			var client = new ElasticClient (config);

Album a500 = new Album { 
	Title = "Aquemini", 
	Artist = "OutKast",
	Rank = 500,
	//...					
};
Album a499 = new Album {
	Title = "Live in Cook County Jail", 
	Artist = "B. B. King",
	Rank = 499,
	//...					
};
Album a498 = new Album {
	Title = "The Stone Roses",
	Artist = "The Stone Roses",
	Rank = 498,
	//...					
};

IBulkResponse response = client.Bulk(br => br
    .Index<Album>(bid => bid
        .Index("rolling-stone-500")
        .Id(a500.Rank)
        .Document(a500)
	    )
    .Index<Album>(bid => bid
        .Index("rolling-stone-500")
        .Id(a499.Rank)
        .Document(a499)
		)
    .Index<Album>(bid => bid
        .Index("rolling-stone-500")
        .Id(a498.Rank)
        .Document(a498)
		)
    // ... etc ...
			                                  );



			ExamineResponse(response);
		}

		public static void Bulk_Index_DefaultIdx(string url)
		{
			var uri = new Uri (url);
			var config = new ConnectionSettings (uri);
			config.SetDefaultIndex("rolling-stone-500");
			var client = new ElasticClient (config);

			Album a1 = new Album ();
			Album a2 = new Album ();
			Album a3 = new Album ();

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
			var uri = new Uri (url);
			var config = new ConnectionSettings (uri);
			config.SetDefaultIndex("rolling-stone-500");
			var client = new ElasticClient (config);

			List<Album> albums = new List<Album> {
				new Album (),
				new Album (),
				new Album (),
			};
            
			var bulkDescriptor = new BulkDescriptor ();
			foreach (var album in albums) {
				bulkDescriptor.Index<Album>(bid => bid
                    .Id(album.Rank)
                    .Document(album)
				);
			}

			var result = client.Bulk(bulkDescriptor);
		}        


        class Album2
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Year { get; set; }
        }

        public static void Bulk_Index_Error(string url)
        {
            var uri = new Uri(url);
            var config = new ConnectionSettings(uri);
            config.SetDefaultIndex("rolling-stone-500");
            var client = new ElasticClient(config);

            var response = client.Bulk(br => br
                .Index<Album2>(bid => bid
                    .Id(499)
                    .Type("album")
                    .Document(new Album2 { 
                        Artist = "foo",
                        Title = "bar",
                        Year = "not an integer"
                    })
            ));

            ExamineResponse(response);
        }

		private static void ExamineResponse(IBulkResponse response)
		{
			Console.WriteLine("Errors: {0}", response.Errors);

			Console.WriteLine("ItemsWithErrors:");
			foreach (BulkOperationResponseItem item in response.ItemsWithErrors) {
                Console.WriteLine("\tId: {0}", item.Id);
                Console.WriteLine("\t\tIndex: {0}", item.Index);
                Console.WriteLine("\t\tType: {0}", item.Type);
                Console.WriteLine("\t\tStatus: {0}", item.Status);                
                Console.WriteLine("\t\tError: {0}", item.Error);
                
			}
            
			Console.WriteLine();
		}
	}
}
