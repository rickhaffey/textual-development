using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextualDevelopment.NEST
{
    public static class NEST_Util
    {
        public static ElasticClient GetClient(string url)
        {
            var uri = new Uri(url);
            var config = new ConnectionSettings(uri);
            
            config.SetDefaultIndex("rolling-stone-500");
            config.ExposeRawResponse(true);

            var client = new ElasticClient(config);

            return client;
        }
    }
}
