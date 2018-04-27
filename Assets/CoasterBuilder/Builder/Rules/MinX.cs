using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Rules
{
    class MinX : Rule
    {
        public bool Pass(List<Track> track, Track NewTrack, List<int> chunks)
        {
            if (NewTrack.Position.X < 0)
                return false;
            else
                return true;
        }

        public override string ToString()
        {
            return "MinX";
        }
    }
}
