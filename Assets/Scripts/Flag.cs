using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D col)
    {
        //print("Colidiu " + col.gameObject.name);
        if (col.gameObject.tag.Equals("Block")) {
            Block blockScript = col.GetComponent<Block>();
            if (!blockScript.controlable)
            {
                //print("Jogador " + blockScript.player + " atingiu o alvo");
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.color = new Color(1, 1, 1, 0.2f);
            }
        }
    }
}
