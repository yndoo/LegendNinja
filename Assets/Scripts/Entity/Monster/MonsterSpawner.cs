using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private int stage = 1;

    private MonsterDatabase monsterDB;

    void Start()
    {
        monsterDB = DataTableLoader.LoadMonsterData("Stage1_MonsterTable");
    }   
}
