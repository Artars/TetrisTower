using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PowerUpInventory
{

    public List<PowerUp> powerUps;
    private int storedPowerUps = 0;
    public static readonly int maxPowerUp;

    public PowerUpInventory()
    {
        powerUps = new List<PowerUp>(); 
    }

    public void pushPowerUp(PowerUp power)
    {
        powerUps.Add(power);
        if(powerUps.Count > 3)
        {
            powerUps.RemoveAt(0);
        }
    }

    public PowerUp popPowerUp(int index)
    {
        if(index > powerUps.Count || index < 0)
        {
            return null;
        }
        PowerUp requested = powerUps[index];
        powerUps.RemoveAt(index);
        return requested;
    }

    public PowerUp GetPowerUp(int index)
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