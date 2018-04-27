using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

	public void CreateTrack ()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void RandomTrack()
    {
        SceneManager.LoadScene("Random");
    }
}
