using System.Collections.Generic;
using Newtonsoft.Json;

namespace Data_Parser_2.Api
{
    public class SearchResponse
    {
        [JsonProperty(PropertyName = "next_page_token")]
        public string NextPageToken { get; set; }

        public List<Result> Results { get; set; }
    }
}