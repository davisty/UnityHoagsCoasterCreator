using CoasterBuilder.Build.Detect;
using CoasterBuilder.Build.Rules;
using CoasterBuilder.Build.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder.Build
{
    public class TaskHandler
    {
        bool firstRun;
        Rule firstRuleBroke = null;
        Task fixTask = null;
        Task Addtional = null;
        Coaster coaster;
        float lastTrackPitch = 0;
        public TaskHandler (Coaster _coaster)
        {
            coaster = _coaster;
        }

        public TaskResults Start(Task task)
        {
            
            TaskResults results = new TaskResults(task);

            firstRun = true;

            results.Pass = RunTask(task, ref results.RuleBroke, ref results.FixTask);
            results.Addtional = Addtional;

            return results;
        }

    

        private bool RunTask(Task task,ref Rule ruleBroke,ref Task fixTask)
        {

            //If Coaster Finshed
            if (coaster.GetCurrentTracksFinshed && task.GetType() != typeof(RemoveChunk))
                return false;

            if (coaster.GetCurrentTracksFinshed && task.GetType() == typeof(RemoveChunk))
                coaster.SetTracksFinshed = false;


            //Get Data
            List<Track> tracks = coaster.GetCurrentTracks;
            List<int> chunks = coaster.GetCurrentChunks;
            bool tracksStarted = coaster.GetCurrentTracksStarted;
            bool tracksFinshed = coaster.GetCurrentTracksFinshed;


            //Used In Expection
            if(tracks.Count > 0)
            lastTrackPitch = tracks.Last().Orientation.Pitch;

            //Run Task
            bool passed = task.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);

            //Check Last Tracks Angle
            if (passed)
            {
                Rule angleCheckCustom = new AngleCheckLastTrack();
                if (!angleCheckCustom.Pass(tracks, tracks.Last(), chunks))
                {
                    ruleBroke = angleCheckCustom;
                    passed = false;
                }
            }

            //Successful
            if (passed)
            {
                //Add New Chunk
                int newTracks = tracks.Count - chunks.Sum();
                if (newTracks > 0)
                    chunks.Add(newTracks);

                Successful(tracks, chunks, tracksStarted, tracksFinshed);
                AddtionalChecks(task);
            }
            else
            {
                if (firstRun)
                {
                    firstRuleBroke = ruleBroke;
                    fixTask = Fail(ruleBroke);

                    if (fixTask != null)
                    {
                        //EXCEPTION
                        if (task.GetType() == typeof(BuildDown) && ruleBroke.GetType() == typeof(MinZ) && lastTrackPitch == 0)
                        {
                            return false;
                        }

                        firstRun = false;
                        return RunTask(fixTask, ref ruleBroke, ref fixTask);
                    }
                }
                 return false;

            }
            return true;
           
        }
        private void Successful(List<Track> tracks, List<int> chunks, bool tracksStarted, bool tracksFinshed)
        {
            coaster.SetTracks = tracks;
            coaster.SetChunks = chunks;
            coaster.SetTracksStarted = tracksStarted;
            coaster.SetTracksFinshed = tracksFinshed;
        }
        private Task Fail(Rule ruleBroke)
        {
            if (ruleBroke == null)
                return null;

            RuleWithFix ruleWithFix = Globals.RulesWithFix.Find(x => x.Rule.GetType() == ruleBroke.GetType());

            if (ruleWithFix == null)
                return null;

            else
                return ruleWithFix.Fix;
            
        }

        private void AddtionalChecks(Task task)
        {
            List<Track> tracks = coaster.GetCurrentTracks;
            List<int> chunks = coaster.GetCurrentChunks;
            bool tracksStarted = coaster.GetCurrentTracksStarted;
            bool tracksFinshed = coaster.GetCurrentTracksFinshed;

            Rule ruleBroke = null;


            if (tracksStarted && InFinshArea.Test(tracks, chunks, tracksStarted, tracksFinshed))
            {
                //EXCEPTION
                if (task.GetType() == typeof(RemoveChunk))
                {
                     task.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke);
                     return;
                }

                FinshCoaster finshCoaster = new FinshCoaster();
                tracksFinshed = true;
                if (RunTask(finshCoaster, ref ruleBroke, ref fixTask))
                    Addtional = finshCoaster; 
            }
            else if (AutoLoopDetected.Test(tracks, chunks, tracksStarted, tracksFinshed))
            {
                BuildAutoLoop buildAutoLoop = new BuildAutoLoop();

                if (RunTask(new BuildAutoLoop(), ref ruleBroke, ref fixTask))
                    Addtional = buildAutoLoop;
            }
        }
    }
}
