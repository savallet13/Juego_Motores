using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{


    public Transform[] enemy_spawn_points;
    public GameObject bear;
    private GameObject sp;

    // Use this for initialization
    void Start()
    {
        Spanw_Enemy();
    }
    // Update is called once per frame
    void Update()
    {

    }
    void Spanw_Enemy()
    {
        for (int i = 0; i < enemy_spawn_points.Length; i++)
        {
            Instantiate(bear, enemy_spawn_points[i].transform.position, enemy_spawn_points[i].transform.rotation);
        }
    }
}
