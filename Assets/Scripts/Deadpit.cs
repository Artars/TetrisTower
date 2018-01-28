using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadpit : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform parent = collision.transform.parent;
        if (parent != null)
            Destroy(parent.gameObject);
        else
            Destroy(collision.gameObject);
    }
    
}
