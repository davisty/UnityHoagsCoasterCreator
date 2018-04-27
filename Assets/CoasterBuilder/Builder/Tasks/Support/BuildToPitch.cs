using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    class BuildToPitch
    {
        public static bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke, float _pitch)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();

            bool resolved = false;
            bool up = false;
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
            float pitch = _pitch;

            if (pitch < tracks.Last().Orientation.Pitch)
            {
                if ((tracks.Last().Orientation.Pitch - pitch) < 180)
                    up = false;
                else
                    up = true;
            }
            else
            {
                if (pitch - tracks.Last().Orientation.Pitch < 180)
                    up = true;
                else
                    up = false;
            }

            if (up)
            {
                resolved = TryTrackType(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, pitch, TrackType.Up);
                if (!resolved)
                {
                    up = false;
                    //Reset
                    tracks = coaster.GetCurrentTracks;
                    chunks = coaster.GetCurrentChunks;
                    tracksStarted = coaster.GetCurrentTracksStarted;
                    tracksFinshed = coaster.GetCurrentTracksFinshed;
                    ruleBroke = _ruleBroke;

                    resolved = TryTrackType(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, pitch, TrackType.Down);
                }

            }
            else
            {
                resolved = TryTrackType(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, pitch, TrackType.Down);
                if (!resolved)
                {
                    up = true;
                    //Reset
                    tracks = coaster.GetCurrentTracks;
                    chunks = coaster.GetCurrentChunks;
                    tracksStarted = coaster.GetCurrentTracksStarted;
                    tracksFinshed = coaster.GetCurrentTracksFinshed;
                    ruleBroke = _ruleBroke;

                    resolved = TryTrackType(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, pitch, TrackType.Up);
                }
            }

            if (resolved)
            {
                if (up)
                {
                    resolved = TryTrackType(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, _pitch, TrackType.Up);

                }
                else
                {
                    resolved = TryTrackType(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, _pitch, TrackType.Down);
                }
            }


            return resolved;
        }


        public new static string ToString()
        {
            return "BuildToPitch";
        }

        public static bool TryTrackType(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke, float pitch, TrackType type)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();
            bool buildPass = true;
            commands.Add(new Command(true, type, new Orientation(0, 0, 0)));

            while (tracks.Last().Orientation.Pitch != pitch && buildPass)
            {
                buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
                if (buildPass == false)
                    return false;
            }

            return true;
        }


    }
}
