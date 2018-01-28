using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardsAngle : MonoBehaviour {
    
    public float speed = 5.0F;
    public float angle = 45;

    // Use this for initialization
    void Start () {
        transform.eulerAngles = new Vector3(angle , -90, 90);
    }

    void Update()
    {
        //use sin and cos to work out x and y speed
        transform.position += new Vector3(Mathf.Cos(angle *Mathf.Deg2Rad) * speed * Time.deltaTime, Mathf.Sin(angle * Mathf.Deg2Rad) * speed * Time.deltaTime, 0);     
    }
}
