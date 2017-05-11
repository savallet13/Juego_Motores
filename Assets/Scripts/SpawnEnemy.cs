using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{


    public Transform[] enemy_spawn_points;
    public GameObject bear;
    public SpawnShip sp;
    private int cont_enemy = 0;

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
    public void removeEnemy()
    {
        ++cont_enemy;       
        if (cont_enemy.Equals(enemy_spawn_points.Length)) {
            sp.Show_Pieces_Ship_2();
        }
    }
}
