using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public GvrViewer viewer;
    public GameObject temp;

    public GameObject[] cameras;

    public void VR ()
    {
        viewer.VRModeEnabled = true;

		cameras[0].GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.Both;
		cameras[1].GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.Both;

        EnableView();
        this.gameObject.SetActive(false);
    }
	
	public void Gyro ()
    {
        viewer.VRModeEnabled = false;
        EnableView();

		cameras[0].GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
		cameras[1].GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;

		viewer.VRModeEnabled = false;
        this.gameObject.SetActive(false);
    }

    public void EnableView ()
    {
        foreach(GameObject g in cameras)
        {
            g.SetActive(false);
        }

        cameras[0].SetActive(true);
        cameras[1].SetActive(true);
        cameras[2].SetActive(true);

        // This enables the coaster
        temp.GetComponent<RollerCoasterPlanes>().enabled = true;
    }
}
