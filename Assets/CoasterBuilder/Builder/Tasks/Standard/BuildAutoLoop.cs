using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CoasterBuilder.Build.Tasks
{
    class BuildAutoLoop : Task
    {
        public bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke)
        {
            //Make a Copy
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

            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();

            BuildLoop buildLoop = new BuildLoop();
            RemoveChunk removeChunk = new RemoveChunk();
            bool successful = false;

            //Test
            for(int i = 0; i < 14; i++)
                removeChunk.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
            successful = buildLoop.Run(tracks, chunks,ref tracksStarted, ref tracksFinshed, ref ruleBroke);

            if (successful)
            {
                for (int i = 0; i < 14; i++)
                    removeChunk.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);
                return buildLoop.Run(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "BuildAutoLoop";
        }
    }
}
