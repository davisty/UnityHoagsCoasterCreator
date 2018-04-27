using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Build.Rules
{
    class Collison : Rule
    {
        public bool Pass(List<Track> tracks, Track NewTrack, List<int> chunks)
        {
            float newTrackStartX = NewTrack.Position.X + 4;
            float newTrackEndX = NewTrack.Position.X - 4;

            float newTrackStartY = NewTrack.Position.Y + 4;
            float newTrackEndY = NewTrack.Position.Y - 4;

            float newTrackStartZ = NewTrack.Position.Z + 4;
            float newTrackEndZ = NewTrack.Position.Z - 4;

            int count = 0;

            foreach (Track track in tracks)
            {
                count++;

                //Skip check on last 2 tracks. (the tracks behind the tracks we just built)
                if (count > tracks.Count - 2)
                {
                    return true;
                }

                ////Region of New Track
                float trackStartX = track.Position.X + 4;
                float trackEndX = track.Position.X - 4;

                float trackStartY = track.Position.Y + 4;
                float trackEndY = track.Position.Y - 4;

                float trackStartZ = track.Position.Z + 4;
                float trackEndZ = track.Position.Z - 4;



                //Check if In Region
                if (
                    ((newTrackEndX >= trackEndX && newTrackEndX <= trackStartX) ||
                    (newTrackStartX >= trackEndX && newTrackStartX <= trackStartX) ||
                    (newTrackStartX >= trackStartX && newTrackEndX <= trackStartX) ||
                    (newTrackEndX <= trackEndX && newTrackStartX >= trackEndX)) &&

                    ((newTrackEndY >= trackEndY && newTrackEndY <= trackStartY) ||
                    (newTrackStartY >= trackEndY && newTrackStartY <= trackStartY) ||
                    (newTrackStartY >= trackStartY && newTrackEndY <= trackStartY) ||
                    (newTrackEndY <= trackEndY && newTrackStartY >= trackEndY)) &&

                    ((newTrackEndZ >= trackEndZ && newTrackEndZ <= trackStartZ) ||
                    (newTrackStartZ >= trackEndZ && newTrackStartZ <= trackStartZ) ||
                    (newTrackStartZ >= trackStartZ && newTrackEndZ <= trackStartZ) ||
                    (newTrackEndZ <= trackEndZ && newTrackStartZ >= trackEndZ))
                    )
                {
                    return false;
                }
            }

            return true;
        }


        public override string ToString()
        {
            return "Collison";
        }
    }
}
