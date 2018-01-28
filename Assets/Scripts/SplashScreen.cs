using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public string nextMap = "Scenes/MainMenu";
    public float waitTime =3.5f;
    float counter;

	// Use this for initialization
	void Start () {
        counter = waitTime;
	}
	
	// Update is called once per frame
	void Update () {
        counter -= Time.deltaTime;
        if (counter < 0) {
            SceneManager.LoadScene(nextMap);
        }
	}
}
