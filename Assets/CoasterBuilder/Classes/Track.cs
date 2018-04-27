using CoasterBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Build
{
    public enum TrackType {Stright, Up, Down, Left, Right, Custom};

	[System.Serializable]
    public class Track
    {
        public TrackType Tracktype;
        public Vector3 Position;
        public Orientation Orientation;


        public Vector3 StartPosition
        {
            get
            {
                float changeX = (float)(Math.Cos(MathHelper.ToRadians(Orientation.Yaw)) * Math.Cos(MathHelper.ToRadians(Orientation.Pitch)) * Globals.TRACK_LENGTH / 2);
                float changeY = (float)(Math.Sin(MathHelper.ToRadians(Orientation.Yaw)) * Math.Cos(MathHelper.ToRadians(Orientation.Pitch)) * Globals.TRACK_LENGTH / 2);
                float changeZ = (float)(Math.Sin(MathHelper.ToRadians(Orientation.Pitch)) * Globals.TRACK_LENGTH / 2);

                Vector3 v = new Vector3(Position.X - changeX, Position.Y - changeY, Position.Z - changeZ);
                return v;
            }
        }
        public Vector3 EndPosition
        {
            get
            {
                float changeX = (float)(Math.Cos(MathHelper.ToRadians(Orientation.Yaw)) * Math.Cos(MathHelper.ToRadians(Orientation.Pitch)) * Globals.TRACK_LENGTH / 2);
                float changeY = (float)(Math.Sin(MathHelper.ToRadians(Orientation.Yaw)) * Math.Cos(MathHelper.ToRadians(Orientation.Pitch)) * Globals.TRACK_LENGTH / 2);
                float changeZ = (float)(Math.Sin(MathHelper.ToRadians(Orientation.Pitch)) * Globals.TRACK_LENGTH / 2);

                Vector3 v = new Vector3(Position.X + changeX, Position.Y + changeY, Position.Z + changeZ);
                return v;
            }
        }

		public Orientation Rotation
		{
			get
			{
				return Orientation;
			}
		}

        public Track(TrackType trackType, Vector3 postion, Orientation orientation)
        {
            Tracktype = trackType;
            Position = postion.Clone();
            Orientation = orientation.Clone();
        }

        public Track Clone()
        {
            return new Track(Tracktype, Position.Clone(), Orientation.Clone());
        }


        public override bool Equals(System.Object obj)
        {
            // If parameter cannot be cast to ThreeDPoint return false:
            Track t = obj as Track;
            if ((object)t == null)
                return false;
            
            // Return true if the fields match:
            return (base.Equals((Track)t)  && Tracktype == t.Tracktype && Position == t.Position && Orientation == t.Orientation);
        }

        public bool Equals(Track t)
        {
            // Return true if the fields match:
            return (Position.Equals(t.Position) && Orientation.Equals(t.Orientation));
        }

        public override string ToString()
        {
            return String.Format("{0,-7}  {1,-22}  {2,-15}", Tracktype, Position, Orientation);
        }
    }
}
