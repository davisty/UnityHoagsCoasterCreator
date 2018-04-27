using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    class FixMinY : Task
    {
        public bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke)
        {
            if (_tracks.Last().Orientation.Yaw == 0 || _tracks.Last().Orientation.Yaw == 180)
                return false;

            bool resolved = false;
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

            for (int i = 1; i < 15; i++)
            {
                CommandHandeler commandHandeler = new CommandHandeler();
                List<Command> commands = new List<Command>();

                for (int j = 0; j < i; j++)
                    commands.Add(new Command(false, TrackType.Stright));

                float startYaw = _tracks[_tracks.Count - 1 - i].Orientation.Yaw;
                if (startYaw < 90)
                {
                    int totalNewTacks = Math.Abs((int)((startYaw) / Globals.STANDARD_ANGLE_CHANGE));
                    for (int k = 0; k < totalNewTacks; k++)
                        commands.Add(new Command(true, TrackType.Right, new Orientation(0, 0, 0)));
                }
                else if ((startYaw >= 90) && (startYaw < 180))
                {
                    int totalNewTacks = Math.Abs((int)((180 - startYaw) / Globals.STANDARD_ANGLE_CHANGE));
                    for (int k = 0; k < totalNewTacks; k++)
                        commands.Add(new Command(true, TrackType.Left, new Orientation(0, 0, 0)));
                }
                else if (startYaw >= 180 && startYaw < 270)
                {
                    int totalNewTacks = Math.Abs((int)((startYaw - 180) / Globals.STANDARD_ANGLE_CHANGE));
                    for (int k = 0; k < totalNewTacks; k++)
                        commands.Add(new Command(true, TrackType.Right, new Orientation(0, 0, 0)));
                }
                else if (startYaw >= 270)
                {
                    int totalNewTacks =  Math.Abs((int)((360 - startYaw) / Globals.STANDARD_ANGLE_CHANGE));
                    for (int k = 0; k < totalNewTacks; k++)
                        commands.Add(new Command(true, TrackType.Left, new Orientation(0, 0, 0)));
                }
               

            
                if (commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke))
                {
                    commandHandeler.Run(commands, _tracks, _chunks, _tracksFinshed, _tracksStarted, ref _ruleBroke);
                    resolved = true;
                    break;
                }
                else
                {
                    tracks = coaster.GetCurrentTracks;
                    chunks = coaster.GetCurrentChunks;
                    tracksStarted = coaster.GetCurrentTracksStarted;
                    tracksFinshed = coaster.GetCurrentTracksFinshed;
                    ruleBroke = _ruleBroke;
                }
            }

            return resolved;
        }
        public override string ToString()
        {
            return "FixMinX";
        }
    }
}
