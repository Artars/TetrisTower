using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour {

    public PowerUp.Type powerUpType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform parent = collision.transform.parent;
        if (parent != null && parent.gameObject.tag.Equals("Block")) {
            Block blockScript = parent.GetComponent<Block>();

            if(blockScript != null)
            {
                BlockController bc = blockScript.getController();
                if (bc != null) {
                    bc.GetComponent<PowerUpInventory>().pushPowerUp((int)powerUpType);
                    Destroy(gameObject);
                }
            }
        }
    }
}
