using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public int countOfPlayers;

    public int scaleX;
    public int scaleZ;
    void Start()
    {
        for (int i = 0; i < countOfPlayers; i++)
        {
            Instantiate(playerPrefab, new Vector3(Random.Range(-scaleX, scaleX), 3, Random.Range(-scaleZ, scaleZ)), Quaternion.identity) ;
        }
    }
}
