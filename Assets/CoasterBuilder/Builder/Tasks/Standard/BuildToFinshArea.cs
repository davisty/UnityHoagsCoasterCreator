using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace CoasterBuilder.Build.Tasks
{
    class BuildToFinshArea : Task
    {
        public bool Run(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            return GetToFinshArea(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
        }
        public override string ToString()
        {
            return "BuildToFinshArea";
        }

        public bool GetToFinshArea(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke)
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

            //260, 70, 150, 25

			Debug.LogError("HERERERER");

            //Get To Finsh Area
            bool pass = false;
            float x = 260f;
            float y = 5;
            float z = 150;
            int zRange = 100;
            int withIn = 50;

            //Try Many Routes.
            //At LEAST
            //XYZ, XZY, YXZ, YZX, ZXY, ZYX, XY Z, Z XY, XYZ, XZ Y, YZ X. Use 3 Heights, Then Back up 1 Track at a time.
            pass = BuildToY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, y, 50);
            pass = BuildToZ.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, z, 50);
            pass = BuildToX.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, 45);
            RemoveChunk rc = new RemoveChunk();
            for (int i = 0; i < 20; i++)
            {
                //XY
                //SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                coaster.SetTracks = _tracks;
                coaster.SetChunks = _chunks;
                coaster.SetTracksStarted = _tracksStarted;
                coaster.SetTracksFinshed = _tracksFinshed;

                tracks = coaster.GetCurrentTracks;
                chunks = coaster.GetCurrentChunks;
                tracksStarted = coaster.GetCurrentTracksStarted;
                tracksFinshed = coaster.GetCurrentTracksFinshed;
                ruleBroke = _ruleBroke;

                for (int j = 0; j < i; j++)
                    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);

                if (BuildToXY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, y, withIn))
                {
                    for (int j = 0; j < i; j++)
                        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                    BuildToXY.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, y, withIn);
                    return true;
                }

                ////X-Y
                ////SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                //coaster.SetTracks = _tracks;
                //coaster.SetChunks = _chunks;
                //coaster.SetTracksStarted = _tracksStarted;
                //coaster.SetTracksFinshed = _tracksFinshed;

                //tracks = coaster.GetCurrentTracks;
                //chunks = coaster.GetCurrentChunks;
                //tracksStarted = coaster.GetCurrentTracksStarted;
                //tracksFinshed = coaster.GetCurrentTracksFinshed;
                //ruleBroke = _ruleBroke;

                //for (int j = 0; j < i; j++)
                //    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);

                //if (BuildToX.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, withIn) && BuildToY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, y, withIn))
                //{
                //    for (int j = 0; j < i; j++)
                //        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                //    BuildToX.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, withIn);
                //    BuildToY.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, y, withIn);
                //    return true;
                //}

                ////Y-X
                ////SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                //coaster.SetTracks = _tracks;
                //coaster.SetChunks = _chunks;
                //coaster.SetTracksStarted = _tracksStarted;
                //coaster.SetTracksFinshed = _tracksFinshed;

                //tracks = coaster.GetCurrentTracks;
                //chunks = coaster.GetCurrentChunks;
                //tracksStarted = coaster.GetCurrentTracksStarted;
                //tracksFinshed = coaster.GetCurrentTracksFinshed;
                //ruleBroke = _ruleBroke;

                //for (int j = 0; j < i; j++)
                //    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                
                //if (BuildToY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, y, withIn) && BuildToX.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, withIn))
                //{
                //    for (int j = 0; j < i; j++)
                //        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                //    BuildToY.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, y, withIn);
                //    BuildToX.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, withIn);
                //    return true;
                //}

                //XYZ
                //SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                coaster.SetTracks = _tracks;
                coaster.SetChunks = _chunks;
                coaster.SetTracksStarted = _tracksStarted;
                coaster.SetTracksFinshed = _tracksFinshed;

                tracks = coaster.GetCurrentTracks;
                chunks = coaster.GetCurrentChunks;
                tracksStarted = coaster.GetCurrentTracksStarted;
                tracksFinshed = coaster.GetCurrentTracksFinshed;
                ruleBroke = _ruleBroke;

                for (int j = 0; j < i; j++)
                    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                
                if (BuildToXYZ.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, y, z, withIn))
                {
                    for (int j = 0; j < i; j++)
                        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                    BuildToXYZ.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, y, z, withIn);
                    return true;
                }

                //Z - XY
                //SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                coaster.SetTracks = _tracks;
                coaster.SetChunks = _chunks;
                coaster.SetTracksStarted = _tracksStarted;
                coaster.SetTracksFinshed = _tracksFinshed;

                tracks = coaster.GetCurrentTracks;
                chunks = coaster.GetCurrentChunks;
                tracksStarted = coaster.GetCurrentTracksStarted;
                tracksFinshed = coaster.GetCurrentTracksFinshed;
                ruleBroke = _ruleBroke;

                for (int j = 0; j < i; j++)
                    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                
                if (BuildToZ.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, z, withIn) && BuildToXY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, y, withIn))
                {
                    for (int j = 0; j < i; j++)
                        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                    BuildToZ.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, z, withIn);
                    BuildToXY.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, y, withIn);
                    return true;
                }

                ////Z-X-Y
                ////SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                //coaster.SetTracks = _tracks;
                //coaster.SetChunks = _chunks;
                //coaster.SetTracksStarted = _tracksStarted;
                //coaster.SetTracksFinshed = _tracksFinshed;

                //tracks = coaster.GetCurrentTracks;
                //chunks = coaster.GetCurrentChunks;
                //tracksStarted = coaster.GetCurrentTracksStarted;
                //tracksFinshed = coaster.GetCurrentTracksFinshed;
                //ruleBroke = _ruleBroke;

                //for (int j = 0; j < i; j++)
                //    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);

                //if (BuildToZ.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, z, withIn) && BuildToX.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, withIn) && BuildToY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, y, withIn))
                //{
                //    for (int j = 0; j < i; j++)
                //        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                //    BuildToZ.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, z, withIn);
                //    BuildToX.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, withIn);
                //    BuildToY.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, y, withIn);

                //    return true;
                //}

                ////Z-Y-X
                ////SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                //coaster.SetTracks = _tracks;
                //coaster.SetChunks = _chunks;
                //coaster.SetTracksStarted = _tracksStarted;
                //coaster.SetTracksFinshed = _tracksFinshed;

                //tracks = coaster.GetCurrentTracks;
                //chunks = coaster.GetCurrentChunks;
                //tracksStarted = coaster.GetCurrentTracksStarted;
                //tracksFinshed = coaster.GetCurrentTracksFinshed;
                //ruleBroke = _ruleBroke;

                //for (int j = 0; j < i; j++)
                //    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);

                //if (BuildToZ.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, z, withIn) && BuildToX.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, withIn) && BuildToY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, y, withIn))
                //{
                //    for (int j = 0; j < i; j++)
                //        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                //    BuildToZ.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, z, withIn);
                //    BuildToY.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, y, withIn);
                //    BuildToX.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, withIn);

                //    return true;
                //}

                ////X-Z-Y
                ////SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                //coaster.SetTracks = _tracks;
                //coaster.SetChunks = _chunks;
                //coaster.SetTracksStarted = _tracksStarted;
                //coaster.SetTracksFinshed = _tracksFinshed;

                //tracks = coaster.GetCurrentTracks;
                //chunks = coaster.GetCurrentChunks;
                //tracksStarted = coaster.GetCurrentTracksStarted;
                //tracksFinshed = coaster.GetCurrentTracksFinshed;
                //ruleBroke = _ruleBroke;

                //for (int j = 0; j < i; j++)
                //    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke); 
                
                //if (BuildToX.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, withIn) && BuildToZ.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, z, withIn) && BuildToY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, y, withIn))
                //{
                //    for (int j = 0; j < i; j++)
                //        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                //    BuildToX.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, withIn);
                //    BuildToZ.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, z, withIn);
                //    BuildToY.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, y, withIn);

                //    return true;
                //}

                ////Y-Z-X
                ////SetupCoaster(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, i, tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                //coaster.SetTracks = _tracks;
                //coaster.SetChunks = _chunks;
                //coaster.SetTracksStarted = _tracksStarted;
                //coaster.SetTracksFinshed = _tracksFinshed;

                //tracks = coaster.GetCurrentTracks;
                //chunks = coaster.GetCurrentChunks;
                //tracksStarted = coaster.GetCurrentTracksStarted;
                //tracksFinshed = coaster.GetCurrentTracksFinshed;
                //ruleBroke = _ruleBroke;

                //for (int j = 0; j < i; j++)
                //    rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                
                //if (BuildToY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, y, withIn) && BuildToZ.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, z, withIn) && BuildToX.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, x, withIn))
                //{
                //    for (int j = 0; j < i; j++)
                //        rc.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);

                //    BuildToY.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, y, withIn);
                //    BuildToZ.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, z, withIn);
                //    BuildToX.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, x, withIn);

                //    return true;
                //}
                
            }

        return false;
        }

        public void SetupCoaster(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke, int removeChunks, List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            RemoveChunk rc = new RemoveChunk();

            Coaster coaster = new Coaster();
            coaster.SetTracks = _tracks;
            coaster.SetChunks = _chunks;
            coaster.SetTracksStarted = _tracksStarted;
            coaster.SetTracksFinshed = _tracksFinshed;

            tracks = coaster.GetCurrentTracks;
            chunks = coaster.GetCurrentChunks;
            tracksStarted = coaster.GetCurrentTracksStarted;
            tracksFinshed = coaster.GetCurrentTracksFinshed;
            ruleBroke = _ruleBroke;

            for (int j = 0; j < removeChunks; j++)
                rc.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
            
        }

 
    }
}
