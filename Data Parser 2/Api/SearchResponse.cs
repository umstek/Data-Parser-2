using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Data_Parser_2.Api
{
    public class SearchResponse
    {
        [CanBeNull]
        [JsonProperty(PropertyName = "next_page_token")]
        public string NextPageToken { get; set; }

        [ItemNotNull]
        [NotNull]
        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once CollectionNeverUpdated.Global
        public List<Result> Results { get; [UsedImplicitly] set; }
    }
}