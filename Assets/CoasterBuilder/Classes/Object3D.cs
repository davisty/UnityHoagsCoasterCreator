using CoasterBuilder.Types;
using System;
using System.Net;


namespace CoasterBuilder
{
	[System.Serializable]
    public class Object3D
    {
        //public
        public bool Visable;

        private Vector3 scale;
        private Vector3 location;
        private Orientation orientation;

        public bool isdirty = true;

        public Vector3 Scale
        {
            get 
            { 
                return scale; 
            }
            set
            {
                scale = value;
                isdirty = true;
            }
        }

        public Vector3 Location
        {
            get
            { 
                return location; 
            }
            set 
            { 
                location = value;
                isdirty = true;
            }
        }

        public Orientation Orientation
        {
            get 
            { return orientation; }

            set
            {
                orientation = value;
                isdirty = true;
            }
        }

        //Private

        //Constuctor
        public Object3D()
        {
            location = new Vector3();
            orientation = new Orientation();
            scale = new Vector3();
        }

        public override string ToString()
        {
            return ("Location: " + location.ToString() + " Orientation:" + orientation.ToString());
        }

    }
}
