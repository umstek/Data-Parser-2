using System;
using System.Collections.Generic;
using Data_Parser_2.Api;

namespace Data_Parser_2.Serialization
{
    public class CSVSerializer
    {
        public string EncodeHeader()
        {
            return "Name,Latitude,Longitude";
        }

        public string EncodeResult(Result result)
        {
            return $"{result.Name},{result.Geometry.Location.Lat},{result.Geometry.Location.Lng}";
        }

        public string EncodeResults(IEnumerable<string> resultStrings)
        {
            return string.Join(Environment.NewLine, resultStrings);
        }
    }
}