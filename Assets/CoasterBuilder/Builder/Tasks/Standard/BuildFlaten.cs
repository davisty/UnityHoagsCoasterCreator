using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CoasterBuilder.Build.Tasks
{
    class BuildFlaten : Task
    {
        public bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke)
        {
            //Copy
            Coaster coaster = new Coaster();
            coaster.SetTracks = _tracks;
            coaster.SetChunks = _chunks;
            coaster.SetTracksStarted = _tracksStarted;
            coaster.SetTracksFinshed = _tracksFinshed;

            List<Track> tracks = coaster.GetCurrentTracks;
            List<int> chunks = coaster.GetCurrentChunks;
            bool tracksStarted = coaster.GetCurrentTracksStarted;
            bool tracksFinshed = coaster.GetCurrentTracksFinshed;
            Rule ruleBroke = _ruleBroke;


            if (RunFlatten(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke))
            {
                return RunFlatten(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return "BuildFlatten";
        }

        public bool RunFlatten(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            BuildDownWard buildDownWard = new BuildDownWard();
            FixMinZ fixMinZ = new FixMinZ();
            bool passed = false;
            do
            {
                buildDownWard.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
            } while (ruleBroke == null);

            if (true)
            {
                ruleBroke = null;
                passed = fixMinZ.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
            }

            return passed;
        }
    }
}
