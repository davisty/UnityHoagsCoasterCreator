using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoasterBuilder.Types
{
    public class Vector3
    {
        private float x;
        private float y;
        private float z;

        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public Vector3(float _x, float _y, float _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }
        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
        //public IEnumerator GetEnumerator()
        //{
        //    yield return x;
        //    yield return y;
        //    yield return z;
        //}
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return this.GetEnumerator();
        //}

        public Vector3 Clone()
        {
            return new Vector3(x,y,z);
        }
        public override bool Equals(System.Object obj)
        {
            // If parameter cannot be cast to ThreeDPoint return false:
            Vector3 v = obj as Vector3;
            if ((object)v == null)
                return false;
            

            // Return true if the fields match:
            return (x == v.x && y == v.y && z == v.z);
        }

        public bool Equals(Vector3 v)
        {
            // Return true if the fields match:
            return (x == v.x && y == v.y && z == v.z);
        }

        public override string ToString()
        {

            return (string.Format("({0:0.0}, {1:0.0}, {2:0.0})",
                        X,
                        Y,
                        Z));
        }
    }
}
