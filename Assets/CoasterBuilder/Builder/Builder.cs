using CoasterBuilder.Build.Rules;
using CoasterBuilder.Build.Tasks;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityStandardAssets.Cameras;

namespace CoasterBuilder.Build
{
	[System.Serializable]
	public class Builder : MonoBehaviour
    {
		TaskHandler taskHandler;
		TaskResults lastTaskResults = new TaskResults(null);

		public List<string> userRequest = new List<string>();
        //public CameraFollow cam;
        public GameObject cam;
        public BuildSelection b;
        public FreeLookCam whatever;
        //public OrthoSmoothFollow cam;

        public Transform coasterPath;
        public float railWidth = .1f;
		public Material trailMat;

		public RollerCoasterPlanes rcp;
		public GameObject trackPrefab;

        public Coaster Coaster;
		public void Start()
		{
			Coaster = new Coaster ();
			taskHandler = new TaskHandler(Coaster);
			userRequest = new List<string>();

			Globals.BuildRules = SetupBuildRules();
			Globals.RemoveRules = SetupRemoveRules();
			Globals.RulesWithFix = SetupTaskForOnRuleFail();

			CreateStartTracks();
		}

        public void StartBuild(Coaster _Coaster, List<string> _UserRequest)
        {
            taskHandler = new TaskHandler(_Coaster);
            if(_UserRequest != null)
                userRequest = _UserRequest;
            else
                userRequest = new List<string>();

            Coaster = _Coaster;
            Globals.BuildRules = SetupBuildRules();
            Globals.RemoveRules = SetupRemoveRules();
            Globals.RulesWithFix = SetupTaskForOnRuleFail();

            CreateStartTracks();
        }

		public void StartBuild(Coaster _Coaster)
        {
            taskHandler = new TaskHandler(_Coaster);
                userRequest = new List<string>();

            Coaster = _Coaster;
            Globals.BuildRules = SetupBuildRules();
            Globals.RemoveRules = SetupRemoveRules();
            Globals.RulesWithFix = SetupTaskForOnRuleFail();

            CreateStartTracks();
        }


        #region Public Fuctions

        public Coaster GetCoaster()
        {
            return Coaster;
        }

        public  List<string> UserRequest()
        {
            return userRequest;
        }

		public void Straight()
		{
			CreatePoint (BuildStraight ().Pass);
		}
		public void Left()
		{
			CreatePoint (BuildLeft ().Pass);
		}
		public void Right()
		{
			CreatePoint  (BuildRight ().Pass);
		}
		public void Up()
		{
			CreatePoint (BuildUp ().Pass);
		}
		public void Down()
		{
			CreatePoint (BuildDown ().Pass);
		}
        /*
		public void Remove()
		{
			CreatePoint (RemoveChunk ().Pass);
		}
        */

        public void Remove()
        {
            int numToRemove = Coaster.GetCurrentTracks.Count;
            //   numToRemove -= prevTrackNum;
            CreatePoint(RemoveChunk().Pass, true);
        }

        public void Finish()
		{
			CreatePoint (FinshCoaster ().Pass);
            b.ViewSelection();


        }

		Quaternion ConvertToQuaternion(float pitch, float yaw, float roll) {
			Quaternion q;
			float t0 = Mathf.Cos(yaw * 0.5f);
			float t1 = Mathf.Sin(yaw * 0.5f);
			float t2 = Mathf.Cos(roll * 0.5f);
			float t3 = Mathf.Sin(roll * 0.5f);
			float t4 = Mathf.Cos(pitch * 0.5f);
			float t5 = Mathf.Sin(pitch * 0.5f);

			q.w = t0 * t2 * t4 + t1 * t3 * t5;
			q.x = t0 * t3 * t4 - t1 * t2 * t5;
			q.y = t0 * t2 * t5 + t1 * t3 * t4;
			q.z = t1 * t2 * t4 - t0 * t3 * t5;
			return q;
		}

//		Matrix4x4 rotWorldMatrix;
//		void rotateAroundWorldAxis(GameObject obj, Vector3 axis, Double radians) {
//			rotWorldMatrix = new Matrix4x4();
//			rotWorldMatrix.makeRotationAxis(axis.normalized, radians);
//			rotWorldMatrix.multiply(object.matrix);                // pre-multiply
//
//			obj.matrix = rotWorldMatrix;
//			object.rotation.setEulerFromRotationMatrix(object.matrix);
//		}

		public void GenerateRandomTracks (int amount)
		{
			for(int i = 0; i < amount; i++)
			{
				switch(UnityEngine.Random.Range(0, 4))
				{
					case 4:
						Debug.Log("Building Down");
						Down();
						break;
					case 3:
						Debug.Log("Building Up");
						Up();
						break;
					case 2:
						Debug.Log("Building Right");
						Right();
						break;
					case 1:
						Debug.Log("Building Left");
						Left();
						break;
					case 0:
						Debug.Log("Building Straight");
						Straight();
						break;
					default:
						Debug.Log("Our Random.Range is out of spec");
						break;

				}
			}
			Finish();
		}

		Vector3 OriToXYZ(float pitch, float yaw, float roll)
		{
			Vector3 XYZ = Vector3.zero;
			XYZ.x = Mathf.Cos(yaw)*Mathf.Cos(pitch);
			XYZ.y = Mathf.Sin(yaw)*Mathf.Cos(pitch);
			XYZ.z = Mathf.Sin(pitch);
			return XYZ;
		}

		private int prevTrackNum = 0;
        void CreatePoint(bool pass, bool isRemove = false)
        {
            if (pass)
            {
                if (isRemove)
                {
                    int numChildren = coasterPath.childCount;
                    int numTracks = Coaster.GetCurrentTracks.Count;
                    print("Trying to remove the # of: " + (numChildren - numTracks).ToString());
                    for (int i = numChildren - 1; i >= numTracks; i--)
                    {
                        Destroy(coasterPath.GetChild(i).gameObject);
                    }
                    prevTrackNum = Coaster.GetCurrentTracks.Count;
                    //whatever.UpdateCameraPosition = coasterPath.GetChild(coasterPath.childCount - 1).gameObject.transform;
                    //cam.UpdateCameraPosition(coasterPath.GetChild(coasterPath.childCount - 1).gameObject);
//                    whatever.Target = coasterPath.GetChild(coasterPath.childCount - 1).gameObject.transform;
					whatever.Target = coasterPath.GetChild(coasterPath.childCount - 1);
                }
                else
                {
                    List<GameObject> tempTransforms = new List<GameObject>();
                    if (prevTrackNum == 0)
                    {
                        prevTrackNum = Coaster.GetCurrentTracks.Count;

                        foreach (Track t in Coaster.GetCurrentTracks)
                        {
                            //						print("HERE!");
                            //							TransformHolder newT = new TransformHolder();
                            GameObject go = Instantiate(trackPrefab, coasterPath);
                            //						go.transform.localScale = new Vector3(.0025f, .03f, .01f);
                            //						var yaw = t.Orientation.Yaw * (float)(Math.PI) / 180f;
                            //						var pitch = t.Orientation.Pitch * (float)(Math.PI) / 180f;
                            go.transform.position = new Vector3(/*-*/t.Position.X/* + 500*/, t.Position.Z, /*-*/t.Position.Y/* - 500*/);
                            //						go.transform.rotation = ConvertToQuaternion(t.Orientation.Pitch, t.Orientation.Yaw, t.Orientation.Roll);
                            go.transform.rotation = Quaternion.Euler(OriToXYZ(t.Orientation.Pitch, t.Orientation.Yaw, t.Orientation.Roll));
                            //						go.transform.eulerAngles = new Vector2(yaw, pitch);
                            //						go.transform.eulerAngles = new Vector3(-t.Orientation.Pitch, t.Orientation.Yaw, -t.Orientation.Roll);
                            //						float yaw = (t.Orientation.Yaw + 90.0f) * Mathf.PI / 180.0f;
                            //						float pitch = -t.Orientation.Pitch * Mathf.PI / 180f;
                            //						var xAxis = new Vector3(1, 0, 0);
                            //						go.transform.RotateAround(go.transform.position, xAxis, -pitch);
                            //						var yAxis = new Vector3(0, 1, 0);
                            //						go.transform.RotateAround(go.transform.position, yAxis, yaw);
                            //						go.transform.rotation.eulerAngles = new Vector3(t.Orientation.Yaw * (float)(180f / Math.PI), t.Orientation.Pitch * (float)(180.0 / Math.PI), 0);//t.Orientation.Roll * );
                            //						go.transform.Rotate(go.transform.forward*t.Orientation.Roll);
                            //						var blah = Quaternion.LookRotation(new Vector3(t.Orientation.Yaw * (float)(180f / Math.PI), t.Orientation.Pitch * (float)(180.0 / Math.PI), 0));//t.Orientation.Roll * );
                            //						go.transform.rotation = blah;
                            //						go.transform.Rotate(go.transform.up*t.Orientation.Yaw);
                            //						go.transform.Rotate(go.transform.right*t.Orientation.Pitch);
                            //						go.transform.localRotation = ConvertToQuaternion(t.Orientation.Pitch, t.Orientation.Roll, t.Orientation.Yaw);
                            //						go.transform.localRotation.eulerAngles.Set(t.Rotation.Roll, t.Rotation.Yaw, t.Rotation.Pitch);

//                            Debug.LogError("The current rotation: " + t.Orientation);

                            if (rcp.maxHeight < go.transform.position.y)
                                rcp.maxHeight = go.transform.position.y + 15;
                            rcp.railPoints.Add(go.transform);

                            tempTransforms.Add(go);
                        }
                    }
                    else
                    {
                        print(Coaster.GetCurrentTracks.Count - prevTrackNum);
                        for (int i = prevTrackNum; i < Coaster.GetCurrentTracks.Count; i++)
                        {
                            //							print("!!!!");
                            GameObject go = Instantiate(trackPrefab, coasterPath);
                            var t = Coaster.GetCurrentTracks[i];
                            go.transform.position = new Vector3(Coaster.GetCurrentTracks[i].Position.X, Coaster.GetCurrentTracks[i].Position.Z, Coaster.GetCurrentTracks[i].Position.Y);

                            //						float yaw = (t.Orientation.Yaw + 90.0f) * Mathf.PI / 180.0f;
                            //						float pitch = -t.Orientation.Pitch * Mathf.PI / 180f;
                            //						float xzLen = Mathf.Cos(pitch);
                            //						var xAxis = new Vector3(1, 0, 0);
                            //						go.transform.RotateAround(go.transform.position, xAxis, -pitch);
                            //						var yAxis = new Vector3(0, 1, 0);
                            //						go.transform.RotateAround(go.transform.position, yAxis, yaw);
                            go.transform.rotation = Quaternion.Euler(OriToXYZ(t.Orientation.Pitch, t.Orientation.Yaw, t.Orientation.Roll));
                            //						go.transform.rotation = Quaternion.Euler(Coaster.GetCurrentTracks[i].Orientation.Pitch, Coaster.GetCurrentTracks[i].Rotation.Yaw, Coaster.GetCurrentTracks[i].Rotation.Roll);
                            //						go.transform.localRotation = Quaternion.Euler(Coaster.GetCurrentTracks[i].Orientation.Roll, Coaster.GetCurrentTracks[i].Orientation.Pitch, Coaster.GetCurrentTracks[i].Orientation.Yaw);
//                            Debug.Log("Rotation: " + Coaster.GetCurrentTracks[i].Orientation);

                            // Anytime a button is pressed, not the start

                            rcp.railPoints.Add(go.transform);

                            if (rcp.maxHeight < go.transform.position.y)
                                rcp.maxHeight = go.transform.position.y + 30;

                            tempTransforms.Add(go);
                            whatever.Target = go.transform;
                        }
                    }

                    DrawTrackPiece(tempTransforms);

                    prevTrackNum = Coaster.GetCurrentTracks.Count - 1;
                    //				print("Prev count: " + Coaster.GetCurrentTracks.Count);
                    var tempStart = Coaster.GetCurrentTracks[Coaster.GetCurrentTracks.Count - 2];
                    var tempEnd = Coaster.GetCurrentTracks[Coaster.GetCurrentTracks.Count - 1];

                    Vector3 start = new Vector3(tempStart.EndPosition.X, tempStart.EndPosition.Y, tempStart.EndPosition.Z);
                    Vector3 startRot = new Vector3(tempStart.Orientation.Yaw, tempStart.Orientation.Pitch, tempStart.Orientation.Roll);
                    Vector3 end = new Vector3(tempEnd.EndPosition.X, tempEnd.EndPosition.Y, tempEnd.EndPosition.Z);
                    Vector3 endRot = new Vector3(tempEnd.Orientation.Yaw, tempEnd.Orientation.Pitch, tempEnd.Orientation.Roll);

                    // This is where the coaster maker should take over
                    //                Debug.CreatePoint(start, end, Color.red);
                    //                Debug.Log("Drawing a line from Vector 3 " + start.ToString() + " to Vector 3 " + end.ToString());

                }
            }
            else
            {
                Debug.Log("Failed!");
            }
		}

        public TaskResults BuildStraight()
        {
            userRequest.Add("BuildStraight()");

            lastTaskResults = taskHandler.Start(new BuildStraight());
            return lastTaskResults;
        }
        public TaskResults BuildLeft()
        {
            userRequest.Add("BuildLeft()");

            lastTaskResults = taskHandler.Start(new BuildLeft());
            return lastTaskResults;
        }

        public TaskResults BuildRight()
        {
            userRequest.Add("BuildRight()");

            lastTaskResults = taskHandler.Start(new BuildRight());
            return lastTaskResults;
        }
        public TaskResults BuildUp()
        {
            userRequest.Add("BuildUp()");

            lastTaskResults = taskHandler.Start(new BuildUp());
            return lastTaskResults;
        }
        public TaskResults BuildDown()
        {
            userRequest.Add("BuildDown()");

            lastTaskResults = taskHandler.Start(new BuildDown());
            return lastTaskResults;
        }
        public TaskResults BuildLoop()
        {
            userRequest.Add("BuildLoop()");

            lastTaskResults = taskHandler.Start(new BuildLoop());
            return lastTaskResults;
        }
        public TaskResults BuildUpward()
        {
            userRequest.Add("BuildUpward()");
            lastTaskResults = taskHandler.Start(new BuildUpward());
            return lastTaskResults;
        }
        public TaskResults BuildDownward()
        {
            userRequest.Add("BuildDownward()");
            lastTaskResults = taskHandler.Start(new BuildDownWard());
            return lastTaskResults;
        }
        public TaskResults BuildFlat()
        {
            userRequest.Add("BuildFlat()");
            lastTaskResults = taskHandler.Start(new BuildFlaten());
            return lastTaskResults;
        }

        public TaskResults BuildLevel()
        {
            userRequest.Add("BuildLevel()");
            lastTaskResults = taskHandler.Start(new BuildLevel());
            return lastTaskResults;
        }
        public TaskResults RemoveChunk()
        {
            userRequest.Add("RemoveChunk()");
            lastTaskResults = taskHandler.Start(new RemoveChunk());
            return lastTaskResults;
        }
        public TaskResults FinshCoaster()
        {
            userRequest.Add("FinshCoaster()");
            lastTaskResults = taskHandler.Start(new BuildToFinshArea());
            return lastTaskResults;
        }
        public TaskResults LastTaskResults()
        {
            return lastTaskResults;
        }
        #endregion

        #region Setup
        private void CreateStartTracks()
        {
            if (!Coaster.GetCurrentTracksStarted)
            {
                userRequest.Add("CreateStartTracks()");
                lastTaskResults = taskHandler.Start(new BuildStartTracks());
                if (lastTaskResults.Pass)
				{
                    Coaster.SetTracksStarted = true;
					CreatePoint (true);
				}
            }
        }
        private List<Rule> SetupBuildRules()
        {
            List<Rule> buildRules = new List<Rule>();
            buildRules.Add(new MinX());
            buildRules.Add(new MaxX());
            buildRules.Add(new MinY());
            buildRules.Add(new MaxY());
            buildRules.Add(new MinZ());
            buildRules.Add(new AngleCheck());
            buildRules.Add(new Collison());

            return buildRules;
        }


        private List<Rule> SetupRemoveRules()
        {
            List<Rule> removeRules = new List<Rule>();

            removeRules = new List<Rule>();
            removeRules.Add(new StartTracksCheck());

            return removeRules;
        }
        private List<RuleWithFix> SetupTaskForOnRuleFail()
        {
            //NOTE: Not All Rules have a fix task. If they do, add the rule, and there fix task

            List<RuleWithFix> rulesWithFix = new List<RuleWithFix>();

            rulesWithFix.Add(new RuleWithFix(new MinX(), new FixMinX()));
            rulesWithFix.Add(new RuleWithFix(new MaxX(), new FixMaxX()));
            rulesWithFix.Add(new RuleWithFix(new MinY(), new FixMinY()));
            rulesWithFix.Add(new RuleWithFix(new MaxY(), new FixMaxY()));
            rulesWithFix.Add(new RuleWithFix(new MinZ(), new FixMinZ()));
            rulesWithFix.Add(new RuleWithFix(new Collison(), new FixTrackCollison()));

            return rulesWithFix;
        }
        #endregion

		int indexName = 0;
		void DrawTrackPiece (List<GameObject> railPoints)
		{
			MeshFilter tempMeshF;
			MeshRenderer meshRender;

			Vector3[] points=new Vector3[4];

			for(int i = 0; i < railPoints.Count - 1; i++)
			{
				var child = railPoints[i].gameObject;
				child.name = "Rail" + (indexName++).ToString();
				child.transform.position = railPoints[i].transform.position;
//				Debug.LogError("Rotation: " + railPoints[i].transform.rotation.eulerAngles);
//				Debug.LogError("LocalRotation: " + railPoints[i].transform.localRotation.eulerAngles);
//				child.transform.Rotate(new Vector3(120, 0, 0));
//				child.transform.rotation = new Quaternion(90, 0, 0, 0);
//				child.transform.localRotation = railPoints[i].transform.localRotation;
//				child.transform.eulerAngles = railPoints[i].transform.eulerAngles;
				child.transform.rotation = railPoints[i].transform.rotation;
//				child.transform.localRotation.eulerAngles.Set(railPoints[i].transform.localRotation.eulerAngles.x, railPoints[i].transform.localRotation.eulerAngles.y, railPoints[i].transform.localRotation.eulerAngles.z);
//				tempMeshF= child.gameObject.AddComponent<MeshFilter>();
//				meshRender= child.gameObject.AddComponent<MeshRenderer>();
//				meshRender.material=trailMat;



//				points[0] = railPoints[i].transform.position -railPoints[i].transform.forward*railWidth;
//				points[1] = railPoints[i].transform.position +railPoints[i].transform.forward*railWidth;
//				points[2] = railPoints[i+1].transform.position -railPoints[i+1].transform.forward*railWidth;
//				points[3] = railPoints[i+1].transform.position +railPoints[i+1].transform.forward*railWidth;
				child.transform.parent = coasterPath;

//				CreateMesh(points[0], points[1], points[2],points[3] ,tempMeshF);
			}

//			GameObject child2 = new GameObject((indexName++).ToString());//"rail");
//			child2.transform.parent=coasterPath;
//			tempMeshF= child2.gameObject.AddComponent<MeshFilter>();
//
//			points[0] = railPoints[railPoints.Count-1].transform.position -railPoints[railPoints.Count-1].transform.forward*railWidth;
//			points[1] = railPoints[railPoints.Count-1].transform.position +railPoints[railPoints.Count-1].transform.forward*railWidth;
//			points[2] = railPoints[0].transform.position -railPoints[0].transform.forward*railWidth;
//			points[3] = railPoints[0].transform.position +railPoints[0].transform.forward*railWidth;
//			meshRender= child2.gameObject.AddComponent<MeshRenderer>();
//			meshRender.material=trailMat;
//
//			CreateMesh(points[0], points[1], points[2],points[3] ,tempMeshF);
		}


		void CreateMesh(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4,  MeshFilter mf)
		{

//			Mesh mesh= new Mesh();
//			mf.mesh=mesh;

			//vertices
//			Vector3[] vertices =new Vector3[4];
//
//			vertices[0]= p1;
//			vertices[1]= p2;
//			vertices[2]= p3;
//			vertices[3]= p4;
//
//			// normal vector
//			Vector3[] normals=new Vector3[4];
//			normals[0]=-Vector3.forward;
//			normals[1]=-Vector3.forward;
//			normals[2]=-Vector3.forward;
//			normals[3]=-Vector3.forward;
//
//			// triangles indices
//			int[] tri=new int[6];
//
//			tri[0]=0;
//			tri[1]=2;
//			tri[2]=1;
//			tri[3]=2;
//			tri[4]=3;
//			tri[5]=1;
//
//			// texture uv
//			Vector2[] uv=new Vector2[4];
//
//			uv[0]=new Vector2(0,0);
//			uv[1]=new Vector2(0,1);
//			uv[2]=new Vector2(1,0);
//			uv[3]=new Vector2(1,1);
//
//			mesh.vertices=vertices;
//			mesh.triangles=tri;
//			mesh.normals=normals;
//			mesh.uv=uv;
		}

    }
}
