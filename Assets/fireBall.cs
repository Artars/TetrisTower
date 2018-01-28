using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour {

    public float angle = -45;
    MoveForwardsAngle mfa;
    // Use this for initialization
    void Awake () {
        mfa = GetComponent<MoveForwardsAngle>();
        mfa.angle = angle;
    }
	
	// Update is called once per frame
	void Update () {

        mfa.angle = angle;
    }

    public static explicit operator fireBall(GameObject v)
    {
        throw new NotImplementedException();
    }
}
