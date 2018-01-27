using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuvem : MonoBehaviour
{
    private float speed=-.1f;

    private void Awake()
    {
        speed= Random.Range(speed*10, speed*1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (transform.position.x < -25)
        {
            transform.position = new Vector3(25, 3.135882f, 0);
        }
    }
}
