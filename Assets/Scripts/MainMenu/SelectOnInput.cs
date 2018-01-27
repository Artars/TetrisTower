using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedGameObj;

    private bool buttonSelected;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("P1_Vertical") !=0 && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedGameObj);
            buttonSelected = true;
        }
	}

    private void onDisable()
    {
        buttonSelected = false;
    }
}
