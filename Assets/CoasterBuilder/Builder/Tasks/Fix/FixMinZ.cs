using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    class FixMinZ : Task
    {
        public bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke)
        {
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

            for (int i = 1; i < 45; i++)
            {
                CommandHandeler commandHandeler = new CommandHandeler();
                List<Command> commands = new List<Command>();

                for(int j = 0; j < i; j++)
                    commands.Add(new Command(false, TrackType.Stright));

                float startPitch = _tracks[_tracks.Count - 1 - i].Orientation.Pitch;

                if (startPitch > 180 && startPitch < 270)
                {
                    int totalNewTacks = (int)((360 - startPitch) / Globals.STANDARD_ANGLE_CHANGE);
                    for (int k = 0; k < totalNewTacks; k++)
                        commands.Add(new Command(true, TrackType.Up, new Orientation(0, 0, 0)));
                }
                else if(startPitch >= 270)
                {
                    int totalNewTacks = (int)((360 - startPitch) / Globals.STANDARD_ANGLE_CHANGE);
                    for (int k = 0; k < totalNewTacks; k++)
                        commands.Add(new Command(true, TrackType.Up, new Orientation(0, 0, 0)));
                }


                if (commands.Count != i)
                {
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

            }

            return resolved;
        }

        public override string ToString()
        {
            return "FixMinZ";
        }
    }
}
