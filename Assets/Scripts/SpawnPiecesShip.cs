using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPiecesShip : MonoBehaviour {


    //ewfwefw
    public Transform [] ship_pieces;
    public GameObject [] piece_ship;
    public Transform tr;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void pieza()
    {
        if (tr.name.Equals("CanoaSpawn"))
        {
            Instantiate(piece_ship[0], ship_pieces[0].position, ship_pieces[0].rotation);
            Instantiate(piece_ship[1], ship_pieces[1].position, ship_pieces[1].rotation);
        }
        else
        {
            Instantiate(piece_ship[0], ship_pieces[0].position, ship_pieces[0].rotation);
            Instantiate(piece_ship[1], ship_pieces[1].position, ship_pieces[1].rotation);
        }
        
    }
}
