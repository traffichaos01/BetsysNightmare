using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class slaughter : MonoBehaviour {

    public Text text;
    public int seconds = 0;
    // Use this for initialization
    void Start()
    {
        text.text = "Congratulations, you have sacrificed " + vars.sacrifices + "  cows, but this will not be enought for poor Raquis Jnr to eat lunch. More must be sacrificed...";
        InvokeRepeating("waitASec", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && seconds > 1)
        {
            SceneManager.LoadScene("CowKiller", LoadSceneMode.Single);
        }
    }

    void waitASec()
    {
        seconds += 1;
    }
}
