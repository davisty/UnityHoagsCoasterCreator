using CoasterBuilder.Build;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelection : MonoBehaviour
{
    public Builder b;
    public GameObject tracks;
    public GameObject view;

    [Range(1, 50)]
    public int amountOfTracks;

    public void Random ()
    {
        b.GenerateRandomTracks(amountOfTracks);
        //this.enabled = false;
        this.gameObject.SetActive(false);
        view.gameObject.SetActive(true);
    }
	
	public void Build ()
    {
        // Disable this UI
        // Enable track selection
        // Add a single straight for camera
        tracks.SetActive(true);
        b.Straight();
        this.gameObject.SetActive(false);
    }

    public void ViewSelection()
    {
        tracks.SetActive(false);
        view.gameObject.SetActive(true);
    }
}
