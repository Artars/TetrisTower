using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    o controller que gera spawn
 */
public class SpawnController : MonoBehaviour {

    List<int> playersIndex;
    public GameObject[] piecesPrefab;
    public List<GameObject> piecesBuffer;

    public SpawnController()
    {
        playersIndex = new List<int>();
        piecesBuffer = new List<GameObject>();
        //piecesPrefab = BlockFabric.getPrefabArray();
    }


    private void Awake()
    {
        piecesBuffer.Add(spawnBlock());
    }
    /*
        0 para player 1
        1 para player 0
     */
    public GameObject getNextBlock(int player)
    {
        while (player > playersIndex.Count -1)
            playersIndex.Add(0);
        
        GameObject returnObject = piecesBuffer[ playersIndex[player] ];
        ++playersIndex[player];
        if(testTailPop())
        {
            popTail();
        }
        return returnObject;
    }

    bool testTailPop()
    {
        foreach(int index in playersIndex)
        {
            if( index == 0 )
            {
                return false;
            }
        }
        return true;
    }

    void popTail()
    {
        for(int i = 0 ; i < playersIndex.Count ; i++)
        {
            --playersIndex[i];
        }

        piecesBuffer.RemoveAt(0);
    }

    GameObject spawnBlock()
    {
        int randomPrefab = Random.Range(0, piecesPrefab.Length-1);
        return piecesPrefab[randomPrefab];
    }

    

}