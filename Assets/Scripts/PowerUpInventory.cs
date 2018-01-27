using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PowerUpInventory
{

    public List<GameObject> powerUps;
    public static readonly int maxPowerUp;

    public PowerUpInventory()
    {
        powerUps = new List<GameObject>(); 
    }

    public void pushPowerUp(GameObject power)
    {
        powerUps.Add(power);
        if(powerUps.Count > 3)
        {
            powerUps.RemoveAt(0);
        }
    }

    public GameObject popPowerUp(int index)
    {
        if(index > powerUps.Count || index < 0)
        {
            return null;
        }
        GameObject requested = powerUps[index];
        powerUps.RemoveAt(index);
        return requested;
    }

    public GameObject getPowerUp(int index)
    {
        if(index>=0 && index < powerUps.Count)
        {
            return null;
        }
        return powerUps[index];
    }

    public int powerUpCount()
    {
        return powerUps.Count;
    }
}