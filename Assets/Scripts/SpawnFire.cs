using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFire : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject fireBall2;
    bool powerUpSpwaned = false;
    bool powerUpSpwaned2 = false;
    fireBall test;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Transform parent = col.transform.parent;
        if (parent != null && parent.gameObject.tag.Equals("Block"))
        {
            Block blockScript = parent.GetComponent<Block>();
            if (!blockScript.controlable)
            {
                if (!powerUpSpwaned)
                {
                    if (blockScript.player == 1)
                    {
                        powerUpSpwaned = true;
                        Instantiate(fireBall, GameObject.Find("Spawn1").transform.position, fireBall.transform.rotation);
                    }
                }

                if (!powerUpSpwaned2)
                {
                    if (blockScript.player == 2)
                    {
                        powerUpSpwaned2 = true;
                        Instantiate(fireBall2, GameObject.Find("Spawn2").transform.position, fireBall.transform.rotation);
                    }
                }
            }            
        }
       
    }
}