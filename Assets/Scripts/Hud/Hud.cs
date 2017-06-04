using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour {

    //Publics
    public Slider slider_life;
    public Slider slider_food;
    public Text count_pieces;
    public GameObject Barco;
    public GameObject SpanPointBarco;


    //Private
    private int num_piezas;


	// Use this for initialization
	void Start () {
        num_piezas = 0;
        UpdateScore();
    }	
	// Update is called once per frame
	void Update () {
        setFood();
        setLife();      
    }
    public void setLife()
    {
        if(slider_life.value <= 0)
        {
            //You has die
            slider_life.value = 0f;
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            slider_life.value -= 0.01f;
        }
    }
    void setFood()
    {
        if (slider_food.value  <= 0)
        {
            //You has angry, need eat!!
            slider_food.value = 0;
            slider_life.value -= 0.05f;
        }
        else
        {
            slider_food.value -= 0.15f;
        }
        
    }
    public void updateFood(float food)
    {
        slider_food.value += food;
    }
    public void AddScore(int newScoreValue)
    {
        num_piezas += newScoreValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        count_pieces.text = "" + num_piezas;
    }
    void Fin_Game()
    {
        if (num_piezas==3){
            //Sacamos el Barco
            Instantiate(Barco,SpanPointBarco.transform.position, SpanPointBarco.transform.rotation);
            //Cuando coja el barco pantalla de Fin
        }
    }
}
