using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextualDevelopment.Domain.RollingStone
{    
    // this attribute is only needed with NEST when i) we don't have an explicit 'Id' field, and ii) we want
    // another field to be used as the source of the id
    [ElasticType(IdProperty="Rank")]
    public class Album
    {        
        public string Title { get; set; }
        public Uri Url { get; set; }
        public string Artist { get; set; }
        public int Rank { get; set; }
        public string Label { get; set; }
        public int Year { get; set; }
        public string Summary { get; set; }
        public Uri ImageUrl { get; set; }

        public static Album ChronicleV1
        {
            get
            {
                return new Album
                {                    
                    Title = "Chronicle Vol. 1",
                    Url = new Uri("http://www.rollingstone.com/music/lists/500-greatest-albums-of-all-time-20120531/creedence-clearwater-revival-chronicle-vol-1-20120524"),
                    Artist = "Creedence Clearwater Revival",
                    Rank = 59,
                    Label = "Fantasy",
                    Year = 1976,
                    Summary = "Between 1968 and early 1972, CCR rolled out 13 Top 40 songs, which still stand as the most impressive run of hits made by an American ... ",
                    ImageUrl = new Uri("http://assets.rollingstone.com/assets/images/list/c305a673692a96976fce8b404c1bf141b7db621e.jpg")
                };
            }
        }
    }
}
