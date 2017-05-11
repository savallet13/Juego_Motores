using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShip : MonoBehaviour {

    //Array que contiene los puntos donde se encontraran las piezas
    public Transform[] barco_spawn_points;
    //Variables publicas que contienen cada una de las piezas por las que esta compuesto el barco de fuga
    public GameObject ship;
    public GameObject ship_2;
    public GameObject ship_3;
    public GameObject ship_4;
    public GameObject ship_5;
    public GameObject ship_6;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Show_Pieces_Ship_1()//Metodo que nos da las primeras 3 piezas del barco
    {
        Instantiate(ship, barco_spawn_points[0].position, Quaternion.identity);
        Instantiate(ship_2, barco_spawn_points[1].position, Quaternion.identity);
        Instantiate(ship_3, barco_spawn_points[2].position, Quaternion.identity);
    }
    public void Show_Pieces_Ship_2()//etodo que nos da las ultimas piezas para fabricar el barco
    {
        Instantiate(ship_4, barco_spawn_points[3].position, barco_spawn_points[3].rotation);
    }
}
