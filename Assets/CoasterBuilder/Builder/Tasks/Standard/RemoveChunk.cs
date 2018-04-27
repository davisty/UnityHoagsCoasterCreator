using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CoasterBuilder.Build.Tasks
{
    class RemoveChunk : Task
    {
        public bool Run(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();

            int totalChunks = chunks.Count();

            //Remove Track
            commands.Add(new Command(false, TrackType.Stright, new Orientation(0, 0, 0)));

            bool pass = false;

            do
            {
                pass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
            }while(pass && totalChunks == chunks.Count());

            return pass;
        }
        public override string ToString()
        {
            return "RemoveChunk";
        }
    }
}
