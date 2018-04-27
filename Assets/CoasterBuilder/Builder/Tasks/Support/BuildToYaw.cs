using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    class BuildToYaw
    {

        public static bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke, float _yaw)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();

            bool resolved = false;
            bool left = false;
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
            float yaw = _yaw;

            if (yaw <= tracks.Last().Orientation.Yaw)
            {
                if((tracks.Last().Orientation.Yaw - yaw) <= 180)   
                    left = true;
                else
                    left = false;      
            }
            else
            {
                if (yaw - tracks.Last().Orientation.Yaw  <= 180)
                    left = true;
                else
                    left = false;  
            }

            if(left)
            {
                resolved = TryTrackType(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, yaw, TrackType.Left);
                if (!resolved)
                {
                    left = false;
                    //Reset
                    tracks = coaster.GetCurrentTracks;
                    chunks = coaster.GetCurrentChunks;
                    tracksStarted = coaster.GetCurrentTracksStarted;
                    tracksFinshed = coaster.GetCurrentTracksFinshed;
                    ruleBroke = _ruleBroke;

                    resolved = TryTrackType(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, yaw, TrackType.Right);
                }

            }
            else
            {
                resolved = TryTrackType(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, yaw, TrackType.Right);
                if (!resolved)
                {
                    left = true;
                    //Reset
                    tracks = coaster.GetCurrentTracks;
                    chunks = coaster.GetCurrentChunks;
                    tracksStarted = coaster.GetCurrentTracksStarted;
                    tracksFinshed = coaster.GetCurrentTracksFinshed;
                    ruleBroke = _ruleBroke;

                    resolved = TryTrackType(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, yaw, TrackType.Left);
                }
            }

            if (resolved)
            {
                if (left)
                {
                    resolved = TryTrackType(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, _yaw, TrackType.Left);

                }
                else
                {
                    resolved = TryTrackType(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, _yaw, TrackType.Right);
                }
            }
            

            return resolved;
        }


        public new static string ToString()
        {
            return "BuildToYaw";
        }

        public static bool TryTrackType(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke, float yaw, TrackType type)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();
            bool buildPass = true;
            commands.Add(new Command(true, type, new Orientation(0, 0, 0)));

            while (tracks.Last().Orientation.Yaw != yaw && buildPass)
            {
                buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
                if (buildPass == false)
                    return false;
            }

            return true;
        }

   

    }
}
