using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build.Tasks
{
    class FixTrackCollison : Task
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

            float currentYaw = _tracks.Last().Orientation.Yaw;
            float goalYawOne = KeepBetween360Degrees(currentYaw + 90);
            float goalYawTwo = KeepBetween360Degrees(currentYaw - 90);

            int totalNewTacksOne = 0;
            int totalNewTacksTwo = 0;

            for (int i = 1; i < 15; i++)
            {

                {
                    CommandHandeler commandHandeler = new CommandHandeler();
                    List<Command> commands = new List<Command>();

                    for (int j = 0; j < i; j++)
                        commands.Add(new Command(false, TrackType.Stright));

                    float startYaw = _tracks[_tracks.Count - 1 - i].Orientation.Yaw;
                    int newTracks = (int)(calculateDifferenceBetweenAngles(startYaw, goalYawOne) / Globals.STANDARD_ANGLE_CHANGE);

                    for (int k = 0; k < newTracks; k++)
                        commands.Add(new Command(true, TrackType.Right, new Orientation(0, 0, 0)));

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
                // }


                // for (int i = 1; i < 15; i++)
                {
                    CommandHandeler commandHandeler = new CommandHandeler();
                    List<Command> commands = new List<Command>();

                    for (int j = 0; j < i; j++)
                        commands.Add(new Command(false, TrackType.Stright));

                    float startYaw = _tracks[_tracks.Count - 1 - i].Orientation.Yaw;
                    int newTracks = (int)(calculateDifferenceBetweenAngles(startYaw, goalYawTwo) / Globals.STANDARD_ANGLE_CHANGE);

                    for (int k = 0; k < newTracks; k++)
                        commands.Add(new Command(true, TrackType.Left, new Orientation(0, 0, 0)));

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
        private double calculateDifferenceBetweenAngles(double firstAngle, double secondAngle)
        {
            double difference = secondAngle - firstAngle;
            while (difference < -180) difference += 360;
            while (difference > 180) difference -= 360;
            return Math.Abs(difference);
        }
        private float KeepBetween360Degrees(float degrees)
        {
            if (degrees < 0)
                return degrees + 360f;
            else if (degrees >= 360)
                return degrees - 360f;
            else
                return degrees;
        }
        public override string ToString()
        {
            return "FixTrackCollison";
        }
    }
}
