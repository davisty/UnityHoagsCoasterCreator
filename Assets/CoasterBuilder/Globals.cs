using CoasterBuilder.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoasterBuilder
{
    public enum Axis : int { Yaw, Pitch, Roll };
    static public class Globals
    {
        #region Constants

        //Start Postion
        public const float START_X = 0;
        public const float START_Y = 0;
        public const float START_Z = 0;

        public const float START_YAW = 0;
        public const float START_PITCH = 0;
        public const float START_ROLL = 0;

        //Start Build Area (Note: Build area only allows postive values. so size 100. means 0-100)
        public const float BUILD_AREA_SIZE_X = 1000;
        public const float BUILD_AREA_SIZE_Y = 1025;

        //Track
        public const float TRACK_LENGTH = (float)7.7;
        public const float TRACK_HIGHT = (float)1.2;
        public const float TRACK_WIDTH = (float)3;

        //Cart
        public const float CART_HEIGHT = (float)5;
        public const float CART_SCALE = .05f;

        //Angle Amount
        public const float STANDARD_ANGLE_CHANGE = 7.5f;

        //Camera
        public const float CAMERA_HEIGHT = 7f;

        //Physics
        public const float GRAVITY = 9.8f;

        //MinSpeed
        public const float CART_MIN_SPEED = 2.0f;
        #endregion

        #region Varibles
        public static List<Rule> BuildRules;
        public static List<Rule> RemoveRules;
        public static List<RuleWithFix> RulesWithFix;
        #endregion
    }
}
