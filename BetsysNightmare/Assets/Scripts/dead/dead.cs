using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dead : MonoBehaviour {

    public Text text;
    public int seconds = 0;
	// Use this for initialization
	void Start () {
        text.text = "You only sacrificed " + vars.sacrifices + "  cows before dying. Press anything to play again...";
        InvokeRepeating("waitASec", 1.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown && seconds > 1)
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
	}

    void waitASec()
    {
        seconds += 1;
    }
}
