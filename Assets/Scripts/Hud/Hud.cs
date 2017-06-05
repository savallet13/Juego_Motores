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
    

    private bool GameEnd = false;


    //Private
    private int num_piezas;


	// Use this for initialization
	void Start () {
        num_piezas = 0;
        UpdateScore();
       
    }	
	// Update is called once per frame
	void Update () {

        if (GameEnd) return;
        setFood();
        setLife();
        Fin_Game();
        /*Comprobar animación cámara
        if(Input.GetKeyDown(KeyCode.M))
        {
            num_piezas = 4;
        }
        */
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
        //Fin_Game();
    }
    void Fin_Game()
    {
        if (num_piezas==4){
            Instantiate(Barco,SpanPointBarco.transform.position, SpanPointBarco.transform.rotation);
            prevpos = Camera.main.transform.position;
            prerot = Camera.main.transform.eulerAngles;
            GameEnd = true;
            MovePlayer.me.active = false;
            StartCoroutine(MirarBarco());
        }
    }

    // Animación cámara

    private float temporizador = 0f;
    private Vector3 prevpos, prerot;
    private IEnumerator MirarBarco()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(160f, 140f, 250f),Time.deltaTime);
        Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.eulerAngles, new Vector3(60f, 150f, 0f), Time.deltaTime);
        yield return new WaitForSeconds(0.01f);
        temporizador += 0.01f;
        if (temporizador >= 2)
        {
            StartCoroutine(MirarBarcoQuieto());
        }
        else
        {
            StartCoroutine(MirarBarco());
        }
    }

    private IEnumerator MirarBarcoQuieto()
    {
        
        yield return new WaitForSeconds(2f);
        temporizador = 0;
        StartCoroutine(Volver());

        
    }

    private IEnumerator Volver()
    {
        
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, prevpos, Time.deltaTime);
        Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.eulerAngles, prerot, Time.deltaTime);
        yield return new WaitForSeconds(0.01f);
        temporizador += 0.01f;
        if (temporizador < 2.5)
        {
            StartCoroutine(Volver());
        }
        else
        {
            MovePlayer.me.active = true;
        }
        
    }
}
