using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    static public class  BuildToX
    {
        public static bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke, float XPosition, float withIn)
        {
            CommandHandeler commandHandeler = new CommandHandeler();

            List<Command> commands = new List<Command>();

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

            bool passed = false;
            bool left = true;
            float yawGoal = 0;


            //Goal Yaw
            if (XPosition > tracks.Last().Position.X)
                yawGoal = 0;
            else
                yawGoal = 180;

            left = true;

            if (GoToX(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, XPosition, withIn, TrackType.Left, yawGoal))
            {
                    passed = GoToX(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, XPosition, withIn, TrackType.Left, yawGoal);
            }

            if (!passed)
            {
                tracks = coaster.GetCurrentTracks;
                chunks = coaster.GetCurrentChunks;
                tracksStarted = coaster.GetCurrentTracksStarted;
                tracksFinshed = coaster.GetCurrentTracksFinshed;
                ruleBroke = _ruleBroke;

                if (GoToX(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, XPosition, withIn, TrackType.Right, yawGoal))
                {
                    passed = GoToX(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, XPosition, withIn, TrackType.Right, yawGoal);
                }
            }

    
            return passed;
        }
        public new static string ToString()
        {
            return "BuildToX";
        }

        static bool GoToX(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke, float XPosition, float withIn, TrackType type, float yawGoal)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();
            bool buildPass = true;

            bool firstStrightTrack = true;
            float lastX = 0;
            float lastDiffernce = 0;


            buildPass = BuildToPitch.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, 0);
            if (!buildPass)
                return false;

            while (!((tracks.Last().Position.X < XPosition + (withIn / 2) && tracks.Last().Position.X > XPosition - (withIn / 2))) && buildPass)
            {
                if (tracks.Last().Orientation.Yaw == yawGoal)
                {
                    commands.Clear();
                    commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);

                    float differnce = Math.Abs(tracks.Last().Position.X - lastX);
                    if (!firstStrightTrack)
                    {
                        //This Means You Passed The Goal Point, This could have been done by turning, Or After the Fact. But You Are now going the wrong way.
                        if (differnce > lastDiffernce)
                            return false;
                    }
                    else
                        firstStrightTrack = true;

                    lastX = tracks.Last().Position.X;
                    lastDiffernce = differnce;

                }
                else
                {
                    commands.Clear();
                    commands.Add(new Command(true, type, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);


                  
                }

            }
            if (tracks.Last().Position.X < XPosition + (withIn / 2) && tracks.Last().Position.X > XPosition - (withIn / 2))
                return true;
            else
                return false;

        }
    }
}
