using JetBrains.Annotations;

namespace Data_Parser_2.Api
{
    [UsedImplicitly]
    public class Result
    {
        public string Name { get; [UsedImplicitly] set; }
        public Geometry Geometry { get; [UsedImplicitly] set; }
    }
}