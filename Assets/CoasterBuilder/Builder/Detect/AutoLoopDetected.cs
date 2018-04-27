using System;
using System.Collections.Generic;
using System.Linq;


namespace CoasterBuilder.Build.Detect
{
    static class AutoLoopDetected
    {
        internal static bool Test(List<Track> tracks, List<int> chunks, bool tracksStarted, bool tracksFinshed)
        {
            if (tracks.Count > 42)
            {
                for (int i = 0; i < 42; i++)
                {
                    if (tracks[(tracks.Count - 1) - i].Tracktype != TrackType.Up)
                        return false;
                }
                return true;
            }
            return false;

        }
    }
}
