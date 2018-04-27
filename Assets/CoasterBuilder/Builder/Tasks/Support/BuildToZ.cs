using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    class BuildToZ
    {

        public static bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke, float ZPosition, float withIn)
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
            float pitchGoal = 0;


            //Goal Yaw
            if (ZPosition > tracks.Last().Position.Z)
                pitchGoal = 90;
            else
                pitchGoal = 270;

            if (GoToZ(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, ZPosition, withIn, TrackType.Up, pitchGoal))
            {
                passed = GoToZ(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, ZPosition, withIn, TrackType.Up, pitchGoal);
            }

            if (!passed)
            {
                tracks = coaster.GetCurrentTracks;
                chunks = coaster.GetCurrentChunks;
                tracksStarted = coaster.GetCurrentTracksStarted;
                tracksFinshed = coaster.GetCurrentTracksFinshed;
                ruleBroke = _ruleBroke;

                if (GoToZ(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, ZPosition, withIn, TrackType.Down, pitchGoal))
                {
                    passed = GoToZ(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, ZPosition, withIn, TrackType.Down, pitchGoal);
                }
            }


            return passed;
        }
        public new static string ToString()
        {
            return "BuildToZ";
        }

        static bool GoToZ(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke, float ZPosition, float withIn, TrackType type, float pitchGoal)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();
            bool buildPass = true;

            bool firstStrightTrack = true;
            float lastZ = 0;
            float lastDiffernce = 0;
            while (!((tracks.Last().Position.Z < ZPosition + (withIn / 2) && tracks.Last().Position.Z > ZPosition - (withIn / 2))) && buildPass)
            {
                if (tracks.Last().Orientation.Pitch == pitchGoal)
                {
                    commands.Clear();
                    commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);

                    float differnce = Math.Abs(tracks.Last().Position.Z - lastZ);
                    if (!firstStrightTrack)
                    {
                        //This Means You Passed The Goal Point, This could have been done by turning, Or After the Fact. But You Are now going the wrong way.
                        if (differnce > lastDiffernce)
                            return false;
                    }
                    else
                        firstStrightTrack = true;

                    lastZ = tracks.Last().Position.Z;
                    lastDiffernce = differnce;

                }
                else
                {
                    commands.Clear();
                    commands.Add(new Command(true, type, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
                }

            }
            if (tracks.Last().Position.Z < ZPosition + (withIn / 2) && tracks.Last().Position.Z > ZPosition - (withIn / 2))
                return true;
            else
                return false;

        }

    


    }
}
