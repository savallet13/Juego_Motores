using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {


    public Transform [] food_spawn_points;
    //public float time_spawn;
    public GameObject food;
    private GameObject sp;

	// Use this for initialization
	void Start () {
        Spanw_Food();
    }
	// Update is called once per frame
	void Update () {
		
	}
    void Spanw_Food(){
        for (int i = 0; i < food_spawn_points.Length; i++)
        {
            Instantiate(food, food_spawn_points[i].transform.position, food_spawn_points[i].transform.rotation);
        }
    }
}
