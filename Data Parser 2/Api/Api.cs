using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace Data_Parser_2.Api
{
    public static class Api
    {
        static Api()
        {
            NearbySearchApi = RestService.For<INearbySearchApi>("https://maps.googleapis.com/maps/api/place/nearbysearch");
        }

        public static INearbySearchApi NearbySearchApi { get; }

        public static async Task<List<Result>> FetchAllNearbyPlaces(string location, int radius, string types)
        {
            var results = new List<Result>();

            var responseTask = NearbySearchApi.FindNearby("6.911398,79.870934", 500, "food");
            void OnTaskComplete()
            {
                results.AddRange(responseTask.Result.Results);
                Thread.Sleep(1000);
                if (string.IsNullOrEmpty(responseTask.Result.NextPageToken)) return;
                responseTask = NearbySearchApi.NextPage(responseTask.Result.NextPageToken);
                responseTask.GetAwaiter().OnCompleted(OnTaskComplete);
            }
            responseTask.GetAwaiter().OnCompleted(OnTaskComplete);

            return results;
        }
    }
}
