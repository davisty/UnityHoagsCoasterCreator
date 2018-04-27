using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Build.Detect
{
    static class InFinshArea
    {
        internal static bool Test(List<Track> tracks, List<int> chunks, bool tracksStarted, bool tracksFinshed)
        {
            float x = 260f;
            float y = 5;
            float z = 150;
            int zRange = 100;
            int withIn = 100;

            return (tracks.Last().Position.X < x + (withIn / 2) && tracks.Last().Position.X > x - (withIn / 2)) && (tracks.Last().Position.Y < y + (withIn / 2) && tracks.Last().Position.Y > y - (withIn / 2));
        }
    }
}
