using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{


    public Transform[] enemy_spawn_points;
    public GameObject bear;
    private GameObject sp;
    public GameObject piece;
    public int NumberEnemies = 5;

    private bool oleada = false;

    // Use this for initialization
    void Start()
    {
        Spanw_Enemy();
    }
    // Update is called once per frame
    void Update()


    {
        
        if(oleada == false && NumberEnemies == 0)
        {

            oleada = true;
            Instantiate(piece, new Vector3(725, 52, 213), Quaternion.identity);
            

        }

    }
    void Spanw_Enemy()
    {
        for (int i = 0; i < enemy_spawn_points.Length; i++)
        {
            Instantiate(bear, enemy_spawn_points[i].transform.position, enemy_spawn_points[i].transform.rotation);
        }
    }
}
