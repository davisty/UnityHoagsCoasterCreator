using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Build
{
    class Command
    {
        public bool BuildTrack;
        public TrackType TrackType;
        public Orientation Orientation;

        public Command(bool buildTrack, TrackType trackType, Orientation orientation)
        {
            BuildTrack = buildTrack;
            TrackType = trackType;
            Orientation = orientation;
        }
        public Command(bool buildTrack, TrackType trackType)
        {
            BuildTrack = buildTrack;
            TrackType = trackType;
            Orientation = new Orientation(0,0,0);
        }
    }
}
