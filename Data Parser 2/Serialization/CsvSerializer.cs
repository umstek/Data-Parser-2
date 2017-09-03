using System;
using System.Collections.Generic;
using System.Linq;
using Data_Parser_2.Api;

namespace Data_Parser_2.Serialization
{
    public static class CsvSerializer
    {
        private static string EncodeHeader()
        {
            return "Name,Latitude,Longitude";
        }

        private static string Escape(string s)
        {
            return $"\"{s}\"";
        }

        private static string EncodeResult(Result result)
        {
            return $"{Escape(result.Name)}," +
                   $"{result.Geometry.Location.Lat}," +
                   $"{result.Geometry.Location.Lng}";
        }

        public static string EncodeResults(IEnumerable<Result> results)
        {
            return
                $"{EncodeHeader()}{Environment.NewLine}" +
                $"{string.Join(Environment.NewLine, results.Select(EncodeResult))}";
        }
    }
}