using CoasterBuilder.Build.Rules;
using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CoasterBuilder.Build
{
    class CommandHandeler
    {
        public bool Run(List<Command> commands, List<Track> tracks, List<int> chunks, bool TrackStarted, bool TracksFinshed, ref Rule ruleBroke)
        {

            foreach (Command command in commands)
            {
                if (command.BuildTrack)
                {
                    if (!BuildTrack(tracks, chunks, command.TrackType, command.Orientation, ref ruleBroke, TrackStarted, TracksFinshed))
                    {
                        return false;
                    }
                }
                else if (!command.BuildTrack)
                {
                    if (!RemoveTrack(tracks, chunks, command.TrackType, command.Orientation, ref ruleBroke, TrackStarted, TracksFinshed))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool BuildTrack(List<Track> tracks, List<int> chunks, TrackType trackType, Orientation orientation, ref Rule ruleBroke, bool TrackStarted, bool TracksFinshed)
        {
            Track lastTrack;

            if(tracks.Count > 0)
                lastTrack = tracks.Last().Clone();
            else
                lastTrack = new Track(TrackType.Custom, new Vector3(Globals.START_X, Globals.START_Y, Globals.START_Z), new Orientation(Globals.START_YAW, Globals.START_PITCH, Globals.START_ROLL));

            #region Orientation

            Orientation newOrientation = null;

            if (trackType == TrackType.Stright)
            {
                newOrientation = new Orientation(lastTrack.Orientation.Yaw,
                                            lastTrack.Orientation.Pitch,
                                            lastTrack.Orientation.Roll);
            }

            if (trackType == TrackType.Up)
            {
                newOrientation = new Orientation(lastTrack.Orientation.Yaw ,
                                            lastTrack.Orientation.Pitch + Globals.STANDARD_ANGLE_CHANGE,
                                            lastTrack.Orientation.Roll);
            }

            if (trackType == TrackType.Down)
            {
                newOrientation = new Orientation(lastTrack.Orientation.Yaw,
                                            lastTrack.Orientation.Pitch - Globals.STANDARD_ANGLE_CHANGE,
                                            lastTrack.Orientation.Roll);
            }

            if (trackType == TrackType.Left)
            {

                newOrientation = new Orientation(lastTrack.Orientation.Yaw + Globals.STANDARD_ANGLE_CHANGE,
                                            lastTrack.Orientation.Pitch ,
                                            lastTrack.Orientation.Roll);
            }

            if (trackType == TrackType.Right)
            {
                newOrientation = new Orientation(lastTrack.Orientation.Yaw - Globals.STANDARD_ANGLE_CHANGE,
                                            lastTrack.Orientation.Pitch ,
                                            lastTrack.Orientation.Roll);
            }

            if (trackType == TrackType.Custom)
            {
                newOrientation = new Orientation(lastTrack.Orientation.Yaw + orientation.Yaw,
                                            lastTrack.Orientation.Pitch + orientation.Pitch,
                                            lastTrack.Orientation.Roll + orientation.Roll);
            }


            #endregion

            #region New Postion

            float test = (float)(Math.Cos(MathHelper.ToRadians(newOrientation.Yaw)) * Math.Cos(MathHelper.ToRadians(newOrientation.Pitch)) * Globals.TRACK_LENGTH / 2);
            float test2 = (float)(Math.Sin(MathHelper.ToRadians(newOrientation.Yaw)) * Math.Cos(MathHelper.ToRadians(newOrientation.Pitch)) * Globals.TRACK_LENGTH / 2);


            float NewX = lastTrack.EndPosition.X + (float)(Math.Cos(MathHelper.ToRadians(newOrientation.Yaw)) * Math.Cos(MathHelper.ToRadians(newOrientation.Pitch)) * Globals.TRACK_LENGTH / 2);
            float NewY = lastTrack.EndPosition.Y + (float)(Math.Sin(MathHelper.ToRadians(newOrientation.Yaw)) * Math.Cos(MathHelper.ToRadians(newOrientation.Pitch)) * Globals.TRACK_LENGTH / 2);
            float NewZ = lastTrack.EndPosition.Z + (float)(Math.Sin(MathHelper.ToRadians(newOrientation.Pitch)) * Globals.TRACK_LENGTH / 2);


            Vector3 newPostion = new Vector3(NewX, NewY, NewZ);

            #endregion 

            #region New TrackType

            TrackType newTrackType = trackType;

            #endregion 
            Track newTrack = new Track(newTrackType, newPostion, newOrientation);


            if (!TrackStarted || PassBuildRules(tracks, newTrack, chunks,ref ruleBroke, TracksFinshed))
            {
                tracks.Add(newTrack);
                return true;
            }
            else
                return false;

        }
        private bool PassBuildRules(List<Track> tracks, Track newTrack,  List<int> chunks,ref Rule ruleBroke, bool TracksFinshed)
        {
         //   if(Globals.In_Finsh_Area)

          //  if (TracksFinshed)
           //     return true;

            foreach (Rule rule in Globals.BuildRules)
            {

                if (!rule.Pass(tracks, newTrack, chunks))
                {
                    //OverRide If Doing Tracks Outside BuildArea
                    if (TracksFinshed)
                    {
                        if (!(rule.GetType() == typeof(MinY)))
                        {
                            ruleBroke = rule;
                            return false;
                        }

                    }
                    else
                    {
                        ruleBroke = rule;
                        return false;
                    }


                }
            }
          
            return true;
        }

        private bool RemoveTrack(List<Track> tracks, List<int> chunks, TrackType trackType, Orientation orientation, ref Rule ruleBroke, bool TrackStarted, bool TracksFinshed)
        {
            if (PassRemoveRules(tracks, chunks, ref ruleBroke))
            {
                tracks.Remove(tracks.Last());
                chunks[chunks.Count - 1] = chunks[chunks.Count - 1] - 1;
            
                if (chunks[chunks.Count - 1] == 0)
                {
                    chunks.Remove(chunks.Last());
                }
                return true;
           }
           else
              return false;                

        }
        private bool PassRemoveRules(List<Track> tracks, List<int> chunks, ref Rule ruleBroke)
        {
            foreach (Rule rule in Globals.RemoveRules)
            {
                if (!rule.Pass(tracks, null, chunks))
                {
                    ruleBroke = rule;
                    return false;
                }
            }
            return true;
        }

    }
}
