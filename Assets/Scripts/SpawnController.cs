using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    o controller que gera spawn
 */
public class SpawnController {

    int[] playersIndex;
    int head;
    GameObject[] piecesPrefab;
    GameObject[] piecesBuffer;

    public SpawnController()
    {
        playersIndex = new int[]{0,0};
        head = 1;
        piecesBuffer = new GameObject[50];
        piecesPrefab = BlockFabric.getPrefabArray();

        piecesBuffer[0] = spawnBlock();
    }

    /*
        0 para player 1
        1 para player 0
     */
    public GameObject getNextBlock(int player)
    {
        ++playersIndex[player];
        if(playersIndex[player] == piecesBuffer.Length)
        {
            playersIndex[player] = 0;
        }
        //armazena mais no buffer
        if(playersIndex[player] == head)
        {
            ++head;
            if(head == piecesBuffer.Length)
            {
                head = 0;
            }
            piecesBuffer[head] = spawnBlock();
        }

        return piecesBuffer[playersIndex[player]];
    }



    private GameObject spawnBlock()
    {
        int randomPrefab = Random.Range(0, piecesPrefab.Length - 1);
        return GameObject.Instantiate(piecesPrefab[randomPrefab]);
    }

    

}