using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CoasterBuilder.Build.Rules
{
    class MinZ : Rule
    {
        public bool Pass(List<Track> track, Track NewTrack, List<int> chunks)
        {
            if ((NewTrack.Orientation.Pitch > 90 && NewTrack.Orientation.Pitch < 270) && (NewTrack.Position.Z < (0 + Globals.CART_HEIGHT * -1 * Math.Cos(DegreeToRadian(NewTrack.Orientation.Pitch)))))
                return false;         
            else if (NewTrack.Position.Z < 0)
                return false;
            else 
                return true;
        }
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public override string ToString()
        {
            return "MinZ";
        }
    }
}
