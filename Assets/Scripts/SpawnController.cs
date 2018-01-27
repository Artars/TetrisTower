using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    o controller que gera spawn
 */
public class SpawnController : MonoBehaviour {

    List<int> playersIndex;
    int head;
    public GameObject[] piecesPrefab;
    private GameObject[] piecesBuffer;

    public SpawnController()
    {
        playersIndex = new List<int>();
        head = 0;
        piecesBuffer = new GameObject[50];
        //piecesPrefab = BlockFabric.getPrefabArray();

    }


    private void Awake()
    {
        piecesBuffer[0] = spawnBlock();
    }
    /*
        0 para player 1
        1 para player 0
     */
    public GameObject getNextBlock(int player)
    {
        while (player > playersIndex.Count -1)
            playersIndex.Add(-1);
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
        int randomPrefab = Random.Range(0, piecesPrefab.Length-1);
        return piecesPrefab[randomPrefab];
    }

    

}