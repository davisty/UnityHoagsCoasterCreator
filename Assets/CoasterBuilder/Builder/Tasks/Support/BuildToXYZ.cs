using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    static public class BuildToXYZ
    {
        public static bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke, float XPosition, float YPosition, float ZPosition, float withIn)
        {
            CommandHandeler commandHandeler = new CommandHandeler();

            List<Command> commands = new List<Command>();

            //Copy
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

            bool passed = false;

            if (GoToXYZ(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, XPosition, YPosition, ZPosition, withIn))
            {
                passed = GoToXYZ(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, XPosition, YPosition, ZPosition, withIn);
            }



            return passed;
        }
        public new static string ToString()
        {
            return "BuildToXYZ";
        }

        static bool GoToXYZ(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke, float XPosition, float YPosition, float ZPosition, float withIn)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();
            bool buildPass = true;

            bool firstStrightTrack = true;
            float last = 0;
            float lastDiffernce = 0;
            float yawGoal = 0;
            float pitchGoal = 0;
            float withInZ = 200;
            if (!buildPass)
                return false;

            while (!((tracks.Last().Position.X < XPosition + (withIn / 2) && tracks.Last().Position.X > XPosition - (withIn / 2)) && (tracks.Last().Position.Y < YPosition + (withIn / 2) && tracks.Last().Position.Y > YPosition - (withIn / 2)) && (tracks.Last().Position.Z <= (ZPosition + (withInZ / 2)) && tracks.Last().Position.Z >= (ZPosition - (withInZ / 2)))) && buildPass)
            {
                float x = XPosition - tracks.Last().Position.X;
                float y = YPosition - tracks.Last().Position.Y;
                float z = ZPosition - tracks.Last().Position.Z;

                //Determine Best Yaw
                yawGoal = Convert.ToSingle(Math.Atan2((double)y, (double)x) * 180 / Math.PI);

                if (yawGoal < 0)
                    yawGoal = yawGoal + 360;

                //Get YawGoal To Nearest Angle Game Can Handle
                int totalAdjustments = (int)(yawGoal / Globals.STANDARD_ANGLE_CHANGE);

                if ((yawGoal % 15) > Globals.STANDARD_ANGLE_CHANGE / 2)
                    totalAdjustments++;

                yawGoal = totalAdjustments * Globals.STANDARD_ANGLE_CHANGE;
      
                //If Z to High, Z To Low
                if (tracks.Last().Position.Z <= (ZPosition + (withInZ / 2)) && tracks.Last().Position.Z >= (ZPosition - (withInZ / 2)))
                    pitchGoal = 0;
                else if(z > 0)
                    pitchGoal = 90;
                else
                    pitchGoal = 270;
                //Determine Best Yaw

                if (tracks.Last().Orientation.Yaw == yawGoal && tracks.Last().Orientation.Pitch == pitchGoal)
                {
                    commands.Clear();
                    commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);

                    float xDistance = tracks.Last().Position.X - XPosition;
                    float YDistance = tracks.Last().Position.Y - YPosition;

                    float differnce = Math.Abs(xDistance + xDistance);
                    if (!firstStrightTrack)
                    {
                        //This Means You Passed The Goal Point, This could have been done by turning, Or After the Fact. But You Are now going the wrong way.
                        if (differnce > lastDiffernce)
                            return false;
                    }
                    else
                        firstStrightTrack = true;

                    last = tracks.Last().Position.X + tracks.Last().Position.Y;
                    lastDiffernce = differnce;

                }
                else
                {
                    int yawDirection = 0;
                    int pitchDirection = 0;
                    if (!(tracks.Last().Orientation.Yaw == yawGoal))
                    {
                        if (tracks.Last().Orientation.Yaw - yawGoal > 0)
                        {
                            if (Math.Abs(tracks.Last().Orientation.Yaw - yawGoal) < 180)
                                yawDirection = -1; //Right
                            else
                                yawDirection = 1; //Left

                        }
                        else
                        {
                            if (Math.Abs(yawGoal - tracks.Last().Orientation.Yaw) < 180)
                                yawDirection = 1; //Left
                            else
                                yawDirection = -1; //Right

                        }                      
                    }
                    //
                    if(!(tracks.Last().Orientation.Pitch == pitchGoal))
                    {
                        if (tracks.Last().Orientation.Pitch - pitchGoal > 0)
                        {
                            if ((tracks.Last().Orientation.Pitch - pitchGoal > 360 - tracks.Last().Orientation.Pitch))
                                pitchDirection = 1; //Up
                            else
                                pitchDirection = -1; //Down
                            
                        }
                        else
                        {
                            if ((pitchGoal - tracks.Last().Orientation.Pitch > 360 - pitchGoal))
                                pitchDirection = -1; //Down
                            else
                                pitchDirection = 1; //Up
                        }
                    }
                    commands.Clear();
                    commands.Add(new Command(true, TrackType.Custom, new Orientation(Globals.STANDARD_ANGLE_CHANGE * yawDirection , Globals.STANDARD_ANGLE_CHANGE * pitchDirection, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);

                }

            }
            if ((tracks.Last().Position.X < XPosition + (withIn / 2) && tracks.Last().Position.X > XPosition - (withIn / 2)) && (tracks.Last().Position.Y < YPosition + (withIn / 2) && tracks.Last().Position.Y > YPosition - (withIn / 2)) && (tracks.Last().Position.Z <= (ZPosition + (withIn / 2)) && tracks.Last().Position.Z >= (ZPosition - (withInZ / 2))))
                return true;
            else
                return false;

        }
    }
}

