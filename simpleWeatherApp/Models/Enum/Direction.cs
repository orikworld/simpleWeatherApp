using System.ComponentModel;

namespace simpleWeatherApp.Models.Enum
{
    public enum Direction
    {
        [Description("North")]
        North = 0,
        [Description("North-East")]
        NorthEast = 1,
        [Description("East")]
        East = 2,
        [Description("South-East")]
        SouthEast = 3,
        [Description("South")]
        South = 4,
        [Description("South-West")]
        SouthWest = 5,
        [Description("West")]
        West = 6,
        [Description("North-West")]
        NorthWest = 8
       
    }
}
