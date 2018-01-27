using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBetweenTwoObjects : MonoBehaviour {

    public Transform object1;
    public Transform object2;
    private Transform myTransform;

	// Use this for initialization
	void Start () {
        if (object1 == null || object2 == null)
            Destroy(gameObject);
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        myTransform.position = (object2.position - object1.position) / 2 + object1.position;
	}
}
