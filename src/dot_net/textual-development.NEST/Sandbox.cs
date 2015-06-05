using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextualDevelopment.NEST
{
    public static class Sandbox
    {
        public static void Run(string url)
        {
            var client = GetClient(url);

            client.Index<Foobar>(new Foobar() { Name = "foobar1" }, i => i
                .Index("foo")
                .Type("bar")
                .Id("1")
                );

            client.Index<Foobar>(new Foobar() { Name = "foobar2" }, i => i
                .Index("foo")
                .Type("bar")
                );
            
        }

        public class Foobar
        {
            public string Name { get; set; }
        }

        private static ElasticClient GetClient(string url)
        {
            var uri = new Uri(url);
            var config = new ConnectionSettings(uri);
            config.ExposeRawResponse(true);
            return new ElasticClient(config);            
        }
    }
}
