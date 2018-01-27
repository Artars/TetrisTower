using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    classe responsável por armazenar todos os possíveis blocos

 */
public class BlockFabric : MonoBehaviour
{

    public static GameObject[] prefabArray;

    public static GameObject[] getPrefabArray()
    {
        return prefabArray;
    }

    /*
        Nessa função serão inseridos os blocos a serem fabricados
     */
    public static void initializePrefab()
    {

    }
}