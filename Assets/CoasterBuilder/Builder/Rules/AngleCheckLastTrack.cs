using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Build.Rules
{
    class AngleCheckLastTrack : Rule
    {
        public bool Pass(List<Track> track, Track NewTrack, List<int> chunks)
        {
            if (NewTrack.Orientation.Yaw % Globals.STANDARD_ANGLE_CHANGE != 0.0f)
                return false;
            if (NewTrack.Orientation.Pitch % Globals.STANDARD_ANGLE_CHANGE != 0.0f)
                return false;
            if (NewTrack.Orientation.Roll % Globals.STANDARD_ANGLE_CHANGE != 0.0f)
                return false;

            return true;
        }

        public override string ToString()
        {
            return "Angle Check Last Track";
        }
    }
}
