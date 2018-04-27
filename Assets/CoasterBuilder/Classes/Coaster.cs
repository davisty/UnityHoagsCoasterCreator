using System;
using System.Linq;
using CoasterBuilder.Build;
using CoasterBuilder.Build.Rules;
using CoasterBuilder.Build.Tasks;
using System.Collections.Generic;

namespace CoasterBuilder
{
	[System.Serializable]
     public class Coaster
    {
        #region Clone
        public static List<Track> Clone(List<Track> list)
        {
            List<Track> newList = new List<Track>();

            foreach (Track v in list)
                newList.Add(v.Clone());

            return newList;
        }
        public static List<int> Clone(List<int> list)
        {
            List<int> newList = new List<int>();

            foreach (int v in list)
                newList.Add(v);

            return newList;
        }
        #endregion

        #region Tracks
        private List<Track> tracks = new List<Track>();
        public List<Track> GetCurrentTracks
        {
            get { return Clone(tracks); }
        }
        public List<Track> SetTracks
        {
            set { tracks = value; }
        }
        #endregion

        #region Chunks
        private List<int> chunks = new List<int>();
        public List<int> GetCurrentChunks
        {
            get { return Clone(chunks); }
        }
        public List<int> SetChunks
        {
            set { chunks = value; }
        }
        #endregion

        #region TrackStarted
        private bool trackStarted = false;

        public bool GetCurrentTracksStarted
        {
            get { return trackStarted; }
        }

        public bool SetTracksStarted
        {
            set { trackStarted = value; }
        }
        #endregion

        #region TracksFinshed
        bool tracksFinshed = false;

        public bool GetCurrentTracksFinshed
        {
            get { return tracksFinshed; }
        }

        public bool SetTracksFinshed
        {
            set { tracksFinshed = value; }
        }
        #endregion
    }
}
