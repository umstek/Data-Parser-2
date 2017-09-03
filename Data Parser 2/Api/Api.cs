using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Refit;

namespace Data_Parser_2.Api
{
    public static class Api
    {
        static Api()
        {
            NearbySearchApi =
                RestService.For<INearbySearchApi>("https://maps.googleapis.com/maps/api/place/nearbysearch");
        }

        private static INearbySearchApi NearbySearchApi { get; }

        public static IObservable<List<Result>> FetchAllNearbyPlaces(string location, int radius, string types)
        {
            return NearbySearchApi.FindNearby(location, radius, types)
                .ToObservable()
                .Timeout(TimeSpan.FromSeconds(1))
                .Retry(5)
                .Delay(TimeSpan.FromSeconds(1))
                .ToAsyncEnumerable()
                .Expand(response =>
                    Observable.If(() => !string.IsNullOrEmpty(response.NextPageToken),
                            NearbySearchApi.NextPage(response.NextPageToken)
                                .ToObservable()
                                .Timeout(TimeSpan.FromSeconds(1))
                                .Retry(5)
                                .Delay(TimeSpan.FromSeconds(1))
                                .Repeat()
                                .TakeUntil(DateTimeOffset.Now + TimeSpan.FromSeconds(25))
                                .SkipWhile(searchResponse => searchResponse.Results.IsEmpty())
                                .Take(1)
                                .Delay(TimeSpan.FromSeconds(1))
                        )
                        .ToAsyncEnumerable()
                )
                .ToObservable()
                .Aggregate(new List<Result>(), (list, response) => list.Concat(response.Results).ToList());
        }
    }
}