using System.Threading.Tasks;
using Refit;

namespace Data_Parser_2.Api
{
    public interface INearbySearchApi
    {
        [Get("/json")]
        Task<SearchResponse> FindNearby(string location, int radius, string types,
            string key = "AIzaSyAZepc1GEfKNKiWwHUf7qjG1veGr6PaERo");

        [Get("/json")]
        Task<SearchResponse> NextPage(string pagetoken, string key = "AIzaSyAZepc1GEfKNKiWwHUf7qjG1veGr6PaERo");
    }
}