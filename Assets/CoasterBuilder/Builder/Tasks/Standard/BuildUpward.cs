using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    class BuildUpward : Task
    {
        public bool Run(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            bool buildPass = true;

            if (tracks.Last().Orientation.Pitch != 90)
            {
                buildPass = BuildToPitch.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, 90);
            }
            else
            {
                CommandHandeler commandHandeler = new CommandHandeler();
                List<Command> commands = new List<Command>();

                commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));

                for (int i = 0; i < 3; i++)
                {
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
                    if (buildPass == false)
                        break;
                }
            }

            return buildPass;
        }
        public override string ToString()
        {
            return "BuildUpward";
        }

        //OLD METHOD
        //  CommandHandeler commandHandeler = new CommandHandeler();
            //List<Command> commands = new List<Command>();
            //bool buildPass = true;

            //if (tracks.Last().Orientation.Pitch == 90.0)
            //{
            //    commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));

            //    for (int i = 0; i < 3; i++)
            //    { 
            //        buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
            //        if (buildPass == false)
            //            break;
            //    }
            //}
            //else
            //{
            //    if (tracks.Last().Orientation.Pitch >= 270f || tracks.Last().Orientation.Pitch <= 90)
            //        commands.Add(new Command(true, TrackType.Up, new Orientation(0, 0, 0)));
            //    else
            //        commands.Add(new Command(true, TrackType.Down, new Orientation(0, 0, 0)));



            //    while (tracks.Last().Orientation.Pitch != 90f && buildPass)
            //    {
            //        buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
            //    }
            //}

            //return buildPass;
    }
}
