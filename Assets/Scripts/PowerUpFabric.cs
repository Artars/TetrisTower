using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFabric
{

    public static GameObject[] prefabArray;

    public static GameObject[] getPrefabArray()
    {
        return prefabArray;
    }

    public static GameObject getPrefabPowerUp(int index)
    {
        if(index >= prefabArray.Length || index < 0)
        {
            return null;
        }
        return (GameObject) Object.Instantiate(prefabArray[index]);
    }

    public static int getPrefabCount()
    {
        return prefabArray.Length;
    }

    /*
        Nessa função serão inseridos os blocos a serem fabricados
     */
    public static void initializePrefab()
    {

    }
}