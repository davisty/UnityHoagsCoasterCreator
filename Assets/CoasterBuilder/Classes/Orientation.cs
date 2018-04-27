using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Types
{
    public class Orientation
    {
        private float yaw;
        private float pitch;
        private float roll;

        public float Yaw
        {
            get { return yaw; }
            set { yaw = KeepBetween360Degrees(value); }
        }
        public float Pitch
        {
            get { return pitch; }
            set { pitch = KeepBetween360Degrees(value); }
        }
        public float Roll
        {
            get { return roll; }
            set { roll = KeepBetween360Degrees(value); }
        }

        public Orientation(float _yaw, float _pitch, float _roll)
        {
            yaw = KeepBetween360Degrees(_yaw);
            pitch = KeepBetween360Degrees(_pitch);
            roll = KeepBetween360Degrees(_roll);
        }
        public Orientation()
        {
            yaw = KeepBetween360Degrees(0);
            pitch = KeepBetween360Degrees(0);
            roll = KeepBetween360Degrees(0);
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

        public Orientation Clone()
        {
            return new Orientation(yaw, pitch, roll);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter cannot be cast to ThreeDPoint return false:
            Orientation o = obj as Orientation;
            if ((object)o == null)
                return false;


            // Return true if the fields match:
            return (yaw == o.yaw && pitch == o.pitch && roll == o.roll);
        }

        public bool Equals(Orientation o)
        {
            // Return true if the fields match:
            return (yaw == o.yaw && pitch == o.pitch && roll == o.roll);
        }

        public override string ToString()
        {

            string yawString = string.Format("Yaw: {0:0.0}", yaw);
            string pitchString = string.Format("Pitch: {0:0.0}", pitch);

            return (string.Format("{0,-10} {1,-10}",
                        yawString,
                        pitchString));
        }
    }
}
