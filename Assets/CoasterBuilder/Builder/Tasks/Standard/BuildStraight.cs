using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CoasterBuilder.Build.Tasks
{
    class BuildStraight : Task
    {
        public bool Run(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            CommandHandeler commandHandeler = new CommandHandeler();

            List<Command> commands = new List<Command>();

            commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
            commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
            commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));

            return commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);

        
        }
        public override string ToString()
        {
            return "BuildStraight";
        }
    }
}
