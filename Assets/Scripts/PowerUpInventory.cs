using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PowerUpInventory : MonoBehaviour
{

    public List<GameObject> powerUpsPrefabs;
    public int[] actualPowerUps;
    public static readonly int maxPowerUp;

    public PowerUpInventory()
    {
        actualPowerUps = new int[3]; 
        actualPowerUps[0] = -1;
        actualPowerUps[1] = -1;
        actualPowerUps[2] = -1;
    }

    public void pushPowerUp(int powerType)
    {
        actualPowerUps[2] = actualPowerUps[1];
        actualPowerUps[1] = actualPowerUps[0];
        actualPowerUps[0] = powerType;
    }
    
    public int popPowerUp()
    {
        int requested = -1;
        if (actualPowerUps[0] != -1) {
            requested = actualPowerUps[0];
            actualPowerUps[0] = actualPowerUps[1];
            actualPowerUps[1] = actualPowerUps[2];
            actualPowerUps[2] = -1;
        }
        return requested;
    }

    public int powerUpCount()
    {
        int count = 0;
        int i = 0;
        while (actualPowerUps[i] != -1 && i < 3)
        {
            count++;
            i++;
        }
        return count;
    }
    
}