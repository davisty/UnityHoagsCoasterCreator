using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace CoasterBuilder.Build.Tasks
{
    class FinshCoaster : Task
    {
        public bool Run(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            return FinshTheCoaster(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);

        }
        public override string ToString()
        {
            return "FinshCoaster";
        }


        public bool FinshTheCoaster(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            tracksFinshed = true;

			//Debug.Print("CALLED?!?!?!?!");

            bool pass = true;
            //Finsh Coaster
            //Vars
            BuildFlaten buildFlaten = new BuildFlaten();
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();
            bool buildPass = true;
            commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));

            BuildToXY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, 260, -100, 50);
            buildFlaten.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
            BuildToXY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, 260, -230, 25);
			BuildToXY.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, Globals.START_X, Globals.START_Y, 25);//500, -275, 25);

            commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
            commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
            ruleBroke = null;
            return pass;
        }
    }
}
