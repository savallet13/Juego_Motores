﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {


    public Transform [] food_spawn_points;
    //public float time_spawn;
    public GameObject food;
    private GameObject sp;
    private int indice;
    private float time_rspwn=5f;

	// Use this for initialization
	void Start () {
        Spanw_Food();
        //StartCoroutine(prova(2));
    }
	// Update is called once per frame
	void Update () {
		
	}
    void Spanw_Food(){
        for (int i = 0; i < food_spawn_points.Length; i++)
        {
            Instantiate(food, food_spawn_points[i].transform.position, food_spawn_points[i].transform.rotation).name= food_spawn_points[i].name;
        }
    }
    public void obtainIndex(int index)
    {
        Instantiate(food, food_spawn_points[index].transform.position, food_spawn_points[index].transform.rotation).name = food_spawn_points[index].name;
    }
    public IEnumerator prova(int ii)
    {
        yield return new WaitForSeconds(60);
        obtainIndex(ii);
    }
}
