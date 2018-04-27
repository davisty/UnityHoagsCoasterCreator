using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    class BuildToY
    {
        public static bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke, float YPosition, float withIn)
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
            float yawGoal = 0;


            //Goal Yaw
            if (YPosition > tracks.Last().Position.Y)
                yawGoal = 90;
            else
                yawGoal = 270;

            if (GoToY(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, YPosition, withIn, TrackType.Left, yawGoal))
            {
                passed = GoToY(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, YPosition, withIn, TrackType.Left, yawGoal);
            }

            if (!passed)
            {
                tracks = coaster.GetCurrentTracks;
                chunks = coaster.GetCurrentChunks;
                tracksStarted = coaster.GetCurrentTracksStarted;
                tracksFinshed = coaster.GetCurrentTracksFinshed;
                ruleBroke = _ruleBroke;

                if (GoToY(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, YPosition, withIn, TrackType.Right, yawGoal))
                {
                    passed = GoToY(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, YPosition, withIn, TrackType.Right, yawGoal);
                }
            }


            return passed;
        }
        public new static string ToString()
        {
            return "BuildToY";
        }

        static bool GoToY(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke, float YPosition, float withIn, TrackType type, float yawGoal)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();
            bool buildPass = true;

            bool firstStrightTrack = true;
            float lastY = 0;
            float lastDiffernce = 0;

            buildPass = BuildToPitch.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, 0);
            if (!buildPass)
                return false;

            while (!((tracks.Last().Position.Y < YPosition + (withIn / 2) && tracks.Last().Position.Y > YPosition - (withIn / 2))) && buildPass)
            {
                if (tracks.Last().Orientation.Yaw == yawGoal)
                {
                    commands.Clear();
                    commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);

                    float differnce = Math.Abs(tracks.Last().Position.Y - lastY);
                    if (!firstStrightTrack)
                    {
                        //This Means You Passed The Goal Point, This could have been done by turning, Or After the Fact. But You Are now going the wrong way.
                        if (differnce > lastDiffernce)
                            return false;
                    }
                    else
                        firstStrightTrack = true;

                    lastY = tracks.Last().Position.Y;
                    lastDiffernce = differnce;

                }
                else
                {
                    commands.Clear();
                    commands.Add(new Command(true, type, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);



                }

            }
            if (tracks.Last().Position.Y < YPosition + (withIn / 2) && tracks.Last().Position.Y > YPosition - (withIn / 2))
                return true;
            else
                return false;

        }

    }
}
