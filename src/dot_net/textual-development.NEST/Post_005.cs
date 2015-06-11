using Elasticsearch.Net.ConnectionPool;
using Nest;
using System;
using System.Text;
using TextualDevelopment.Domain.RollingStone;

namespace TextualDevelopment.NEST
{    
	public static class Post_005
	{
		public static void GetAlbum(string url)
		{            
			var uri = new Uri(url);
			var config = new ConnectionSettings(uri);
			config.ExposeRawResponse(true);

			var client = new ElasticClient(config);

			IGetResponse<Album> response = Get_InlineFunc(client);
			ExamineResponse(response);

			IGetResponse<Album> response2 = Get_BadId(client);
			ExamineResponse(response2);

			IGetResponse<Album> response3 = Get_BadIndex(client);
			ExamineResponse(response3);
		}

		private static void ExamineResponse(IGetResponse<Album> response)
		{
			Console.WriteLine("_index: {0}", response.Index);
			Console.WriteLine("_type: {0}", response.Type);
			Console.WriteLine("_id: {0}", response.Id);
			Console.WriteLine("_version: {0}", response.Version);
			Console.WriteLine("found: {0}", response.Found);

			if (response.Source != null) 
			{
				Console.WriteLine();
				Console.WriteLine("SOURCE: ");	
				Console.WriteLine("Title: {0}", response.Source.Title);
				Console.WriteLine("Url: {0}", response.Source.Url);
				Console.WriteLine("Artist: {0}", response.Source.Artist);
				Console.WriteLine("Rank: {0}", response.Source.Rank);
				Console.WriteLine("Label: {0}", response.Source.Label);
				Console.WriteLine("Year: {0}", response.Source.Year);
				Console.WriteLine("Summary: {0}", response.Source.Summary);
				Console.WriteLine("ImageUrl: {0}", response.Source.ImageUrl);
			}
				
			Console.WriteLine();
		}

		private static IGetResponse<Album> Get_InlineFunc(ElasticClient client)
		{
			IGetResponse<Album> response = client.Get<Album>(g => g
				.Index("rolling-stone-500")
				.Id(59)
			);

			return response;
		}

		private static IGetResponse<Album> Get_BadId(ElasticClient client)
		{
			IGetResponse<Album> response = client.Get<Album>(g => g
				.Index("rolling-stone-500")
				.Id(501)
			);

			return response;
		}

		private static IGetResponse<Album> Get_BadIndex(ElasticClient client)
		{
			IGetResponse<Album> response = client.Get<Album>(g => g
				.Index("foo")
				.Id(59)
			);

			return response;
		}
	}
}
