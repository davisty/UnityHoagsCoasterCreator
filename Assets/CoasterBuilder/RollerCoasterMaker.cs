using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CoasterBuilder.Build;
using CoasterBuilder.Ride;
using System.Reflection;
using System.Diagnostics;

namespace CoasterBuilder
{
	[System.Serializable]
	public class RollerCoasterMaker : MonoBehaviour
    {
        public Coaster NewCoaster()
        {
            return new Coaster();
        }

        public Builder Builder(Coaster coaster)
        {
            Builder builder = new Builder();
            List<string> userRequests = new List<string>();
			builder.StartBuild(coaster, userRequests);

            return builder;
        }
        public Rider Rider(Coaster coaster)
        {
            return null;
        }

        public List<Coaster> LoadCoasters()
        {
            return null;
        }
        public Coaster LoadCoaster(string id)
        {
            return null;
        }
        public bool SaveCoaster(Coaster coaster)
        {
            return false;
        }

        public string VersionNum()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
    }
}
