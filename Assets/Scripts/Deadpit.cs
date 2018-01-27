using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadpit : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }

}
