using System;
using System.Collections.Generic;
using System.Linq;

namespace CoasterBuilder.Build.Rules
{
    class MaxX : Rule
    {

        public bool Pass(List<Track> track, Track NewTrack, List<int> chunks)
        {
            if (NewTrack.Position.X > Globals.BUILD_AREA_SIZE_X)
                return false;
            else
                return true;
        }

        public override string ToString()
        {
            return "MaxX";
        }
        

    }
}
