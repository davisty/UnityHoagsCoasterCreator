using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CoasterBuilder.Build.Rules
{
    class StartTracksCheck : Rule
    {
        public bool Pass(List<Track> track, Track NewTrack, List<int> chunks)
        {
            if (chunks.Count <= 1)
                return false;
            else
                return true;
        }

        public override string ToString()
        {
            return "StartTracksCheck";
        }
    }
}
