using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CoasterBuilder.Build.Tasks
{
    class BuildLoop : Task
    {
        public bool Run(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            //Build Upward. First Half Angle Right, Then Angle Left (yaw)


            CommandHandeler commandHandeler = new CommandHandeler();

            List<Command> commands = new List<Command>();

            for (int i = 0; i < 24; i++)
                commands.Add(new Command(true, TrackType.Custom, new Orientation(.5f, Globals.STANDARD_ANGLE_CHANGE, 0)));
            for (int i = 0; i < 24; i++)
                commands.Add(new Command(true, TrackType.Custom, new Orientation(-.5f, Globals.STANDARD_ANGLE_CHANGE, 0)));

            return commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);



            //int x = myTracks.Count;
            //double theLastAngle = 0;
            //Vector3 origen = new Vector3();
            //int tracksBuilt = 0;

            ////Remove Tracks First
            //for (int i = 0; i < (15 * 3); i++)
            //{
            //    if (!(directions.Last() == Direction.Up || directions.Last() == Direction.Special) || myTracks.Last().Orientation.X == 0.0f)
            //    {
            //        break;
            //    }

            //    //Safely Remove from last grouping.
            //    RemoveTracks(1, myTracks, ref directions);
            //}


            //origen = new Vector3(7.5f, .5f, 0);
            //do
            //{
            //    Special = origen;
            //    tracksBuilt += BuildTrack(Direction.Special, myTracks, ref directions);
            //    // BulidChuckAdv(1, 1, Direction.Special, myTracks, ref directions, origen);
            //}
            //while (myTracks.Last().Orientation.X != 180.0);

            //origen = new Vector3(7.5f, -.5f, 0);
            //do
            //{
            //    theLastAngle = myTracks.Last().Orientation.X;
            //    Special = origen;
            //    tracksBuilt += BuildTrack(Direction.Special, myTracks, ref directions);

            //}
            //while (myTracks.Last().Orientation.X != 0.0);

            //float TEST = myTracks.Last().Orientation.X % (float)7.5;
            //float TEST2 = myTracks.Last().Orientation.Y % (float)7.5;


            //if (TEST != 0 || TEST2 != 0)
            //{

            //    float offsetX = 0;

            //    if (TEST != 0)
            //    {
            //        offsetX = myTracks.Last().Orientation.X;

            //        while (TEST < 0)
            //        {
            //            offsetX = offsetX + 360;
            //        }

            //        offsetX = offsetX % (float)7.5;
            //    }

            //    float offsetY = 0;

            //    if (TEST2 != 0)
            //    {
            //        offsetY = myTracks.Last().Orientation.Y;

            //        while (TEST2 < 0)
            //        {
            //            offsetY = offsetY + 360;
            //        }

            //        offsetY = offsetY % (float)7.5;
            //    }



            //    Special = new Vector3(-offsetX, -offsetY, 0);
            //    tracksBuilt += BuildTrack(Direction.Special, myTracks, ref directions);
            //}


            //Chunks.Add(tracksBuilt);



        //    return false;
        }

        public override string ToString()
        {
            return "BuildLoop";
        }
    }
}
