using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Types
{
    static class MathHelper
    {
        public static float ToRadians(float degrees)
        {
            return (float)(Math.PI * degrees / 180.0);
        }

        public static float ToDegree(float radian)
        {
            return (float)(radian * (180.0 / Math.PI));
        }

        public static float PiOver4()
        {
            return (float)(Math.PI / 4);
        }

    }
}
