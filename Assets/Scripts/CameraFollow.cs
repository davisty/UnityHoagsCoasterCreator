using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject track;
    public Vector3 offset;

	public void UpdateCameraPosition (GameObject _track)
    {
        track = _track;

        Debug.Log("Called");
	}

    private void LateUpdate()
    {
        transform.position = track.transform.position + offset;
    }
}
