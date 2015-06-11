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
				Console.WriteLine("_source: ");	
				Console.WriteLine("\tTitle: {0}", response.Source.Title);
				Console.WriteLine("\tUrl: {0}", response.Source.Url);
				Console.WriteLine("\tArtist: {0}", response.Source.Artist);
				Console.WriteLine("\tRank: {0}", response.Source.Rank);
				Console.WriteLine("\tLabel: {0}", response.Source.Label);
				Console.WriteLine("\tYear: {0}", response.Source.Year);
				Console.WriteLine("\tSummary: {0}", response.Source.Summary);
				Console.WriteLine("\tImageUrl: {0}", response.Source.ImageUrl);
			}	

			Console.WriteLine("HttpStatusCode: {0}", response.ConnectionStatus.HttpStatusCode);
			if (response.ConnectionStatus.HttpStatusCode >= 400) 
			{
				Console.WriteLine(Encoding.UTF8.GetString(response.ConnectionStatus.ResponseRaw));	
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
