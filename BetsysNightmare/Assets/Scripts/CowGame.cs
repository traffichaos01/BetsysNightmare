using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CowGame : MonoBehaviour {

    [SerializeField] Text textComponent;
    [SerializeField] State startingState;
    [SerializeField] int hour;
    State state;

    // Use this for initialization
    void Start () {
        state = startingState;
        textComponent.text = state.GetStateStory();

	}
	
	// Update is called once per frame
	void Update () {
        ManageState();
	}
    

    private void ManageState()
    {
        var nextStates = state.GetNextStates();
        int countOptions = state.GetNextStates().Length;
            if (Input.anyKeyDown)
            {
                if (countOptions == 0)
                {
                    HourPlus();
                }
                else if (countOptions == 1)
                {
                    SetState(nextStates[0]);
                }
            }
    }

    void SetState (State newState) {
        state = newState;
        textComponent.text = state.GetStateStory();
    }

    void HourPlus ()
    {
        SceneManager.LoadScene("CowKiller", LoadSceneMode.Single);
    }
}

