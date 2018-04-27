using CoasterBuilder.Build;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoasterBuilder.Types
{
    public enum CameraMode
    {
        LookAt,
        Free,
        FirstPerson
    }

    public class Camera
    {
        #region Varibles
        //Mode
        //Free: you look via your orentaion (yaw, and pitch)
        //Look At: camera always looks at target model, and you can rotate around it
        public CameraMode CameraMode = CameraMode.LookAt;


        public float CameraHeight 
        {
             get 
             {

                 float z = zoom * (float)Math.Sin(Orientation.Y);
                 return z;
             }
        }

        //The Target Model For Look At
        private Vector3 targetModelLocation = new Vector3(0, 0, 0);

        public Vector3 TargetModelLocation
        {
            get { return targetModelLocation; }
            set { targetModelLocation = new Vector3(value.X, value.Y, value.Z); }
        }
        public Object3D FirstPersonObject = new Object3D();

        //Postion (X,Y,Z)
        public Vector3 position = new Vector3(0, 0, 0);

        //Orientation (Yaw, Pitch, Roll)
        private Vector3 orientation_Free = new Vector3(0, 0, 0);
        private Vector3 orientation_FirstPerson = new Vector3(0, 0, 0);
        private Vector3 orientation_LookAt = new Vector3(0, 0, 0);

        //How wide and tall you can see
        public float viewAngle = MathHelper.PiOver4();

        //How Close you can see things (if its closer then the value you wont see it)
        public float nearClip = .5f;

        //How Far you can see things (if its Farther then the value you wont see it)
        public float farClip = 15000f;

        //Zoom
        private float zoom = 155;

        public float Zoom
        {
            get { return zoom; }
            set 
            {

                if (value < 5)
                {
                    zoom = 5;
                }
                else
                {
                    zoom = value;
                }
           }
        }

        #endregion
        #region Transition Varibles
        public bool transition = false;

        float yaw_PerTrans;
        float pitch_PerTrans;
        Vector3 location_PerTrans;
        int totalTransitions;
        int TransitionCount;

        #endregion
        public float Pitch
        {
            get { return Orientation.X * (float)(180.0 / Math.PI); }
            set 
            {
                if (CameraMode.LookAt == CameraMode)
                {
                  //  float z = (float)zoom * -(float)Math.Sin(Math.PI * value / 180.0);
                   // if ((z + targetModelLocation.Y) > .5)
                   // {
                        Orientation.X = (float)(Math.PI * value / 180.0);
                //    }
                //    else
                //    {
                //        Orientation.X = (float)(Math.PI * 0 / 180.0);
                //    }
                }
                else
                {
                    Orientation.X = (float)(Math.PI * value / 180.0);
                }

            }
        }
        public float Yaw
        {
            get { return (Orientation.Y * (float)(180.0 / Math.PI)); }
            set { Orientation.Y = (float)(Math.PI * value / 180.0); }
        }
        public Vector3 Orientation
        {
            get 
            {
                if (CameraMode == CameraMode.FirstPerson)
                {
                    return orientation_FirstPerson;
                }
                else if (CameraMode == CameraMode.Free)
                {
                    return orientation_Free;
                }
                else if (CameraMode == CameraMode.LookAt)
                {
                    return orientation_LookAt;
                }

                return new Vector3();
            
            }
            set
            {
                if (CameraMode == CameraMode.FirstPerson)
                {
                    orientation_FirstPerson = new Vector3(KeepOrientationVaild(MathHelper.ToRadians(value.X)),
                               KeepOrientationVaild(MathHelper.ToRadians(value.Y)),
                               KeepOrientationVaild(MathHelper.ToRadians(value.Z)));
                }
                else if (CameraMode == CameraMode.Free)
                {
                    orientation_Free = new Vector3(KeepOrientationVaild(MathHelper.ToRadians(value.X)),
                               KeepOrientationVaild(MathHelper.ToRadians(value.Y)),
                               KeepOrientationVaild(MathHelper.ToRadians(value.Z)));
                }
                else if (CameraMode == CameraMode.LookAt)
                {
                    orientation_LookAt = new Vector3(KeepOrientationVaild(MathHelper.ToRadians(value.X)),
                               KeepOrientationVaild(MathHelper.ToRadians(value.Y)),
                               KeepOrientationVaild(MathHelper.ToRadians(value.Z)));
                }
            }
        }

        public Camera()
        {
            position = new Vector3(0, 0, 0);
            Orientation = new Vector3(0, 0, 0);
            TargetModelLocation = new Vector3(0, 0, 0);

            CameraMode = CameraMode.Free;
            Orientation = new Vector3(0, 0, 0);

            CameraMode = CameraMode.FirstPerson;
            Orientation = new Vector3(0, 0, 0);

            CameraMode = CameraMode.LookAt;
            Orientation = new Vector3(0, 0, 0);

            Yaw = 200;
            Pitch = -16;

        }

        public void CameraReset()
        {
            position = new Vector3(0, 0, 0);
            Orientation = new Vector3();
        }

        //  Rotate Camera (Free And Look At)
        public void RotateCamera(Axis axis, float angle)
        {
            angle = MathHelper.ToRadians(angle);

            if (axis == Axis.Yaw)
            {
                Orientation.Y += angle;
                Orientation.Y = KeepOrientationVaild(Orientation.Y);
            }

            if (axis == Axis.Pitch)
            {
                Orientation.X += angle;
                Orientation.X = KeepOrientationVaild(Orientation.X);
            }
            if (axis == Axis.Roll)
            {
                Orientation.Z += angle;
                Orientation.Z = KeepOrientationVaild(Orientation.Z);
            }

        }

        //  Move Camera
        public void MoveForward(float speed)
        {
            speed = MathHelper.ToRadians(speed);
            position.Z += speed * (float)(Math.Cos(Orientation.Y) * Math.Cos(Orientation.X));
            position.X += speed * (float)(Math.Sin(Orientation.Y) * Math.Cos(Orientation.X));
            position.Y += -speed * (float)(Math.Sin(Orientation.X));
        }
        public void MoveRight(float speed)
        {
            speed = MathHelper.ToRadians(speed);
            position.Z += -speed * (float)(Math.Sin(Orientation.Y) * Math.Cos(Orientation.X));
            position.X += speed * (float)(Math.Cos(Orientation.Y) * Math.Cos(Orientation.X));
        }
        public void MoveUp(float speed)
        {
            speed = MathHelper.ToRadians(speed);
            position.Y += speed;
        }

        //  Helper Fuction
        private float KeepOrientationVaild(float angle)
        {

            // keep the value in the range 0-360 (0 - 2 PI radians)
            if (angle >= Math.PI * 2)
                angle -= (float)Math.PI * 2;
            else if (angle < 0)
                angle += (float)Math.PI * 2;
            return (angle);
        }

        public void transitionCameraSetup(Vector3 _Location, int _TotalTransitions)
        {
            totalTransitions = _TotalTransitions;
            transition = true;
            TransitionCount = 0;
            location_PerTrans = new Vector3((_Location.X - TargetModelLocation.X) / totalTransitions,
                                                    (_Location.Y - TargetModelLocation.Y) / totalTransitions,
                                                    (_Location.Z - TargetModelLocation.Z) / totalTransitions);
        }

        public void transitionCamera()
        {
            if (TransitionCount == totalTransitions)
            {
                transition = false;
            }
            else
            {
               // Yaw = Yaw + yaw_PerTrans;
               // Pitch = Pitch + pitch_PerTrans;
                TargetModelLocation.X = TargetModelLocation.X + location_PerTrans.X;
                TargetModelLocation.Y = TargetModelLocation.Y + location_PerTrans.Y;
                TargetModelLocation.Z = TargetModelLocation.Z + location_PerTrans.Z;
            }
               TransitionCount++;
        }
    }
}
