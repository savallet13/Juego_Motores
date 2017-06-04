using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    Animator anim;
    Transform player;  // Reference to the player's position.
    NavMeshAgent nav;  // Reference to the nav mesh agent.
    private float ataque =1f;

    public Vector3 posPlayer;
    public float distancia;
    public float distanciaMax = 25f;
    public Slider Slider_Life;
    public Slider Slider_Personaje;
    public Hud hud;



    void Start()
    {
        Slider_Personaje = GameObject.FindGameObjectWithTag("SliderP").GetComponent<Slider>();
    }
    void FixedUpdate()
    {

    }
    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Correr()
    {
        anim.SetBool("Visto", true);
    }
    void Parar()
    {
        anim.SetBool("Visto", false);
    }
    void Update(){
        posPlayer = player.transform.position;

        distancia = Vector3.Distance(posPlayer, this.transform.position); //Obtencion del vector dirección (transform es él mismo en este caso Enemy)

        if (distancia < distanciaMax)
        {
            nav.SetDestination(player.position);
            Correr();
        }
        else
        {
            nav.SetDestination(transform.position);
            Parar();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Atacar();
        }
    }
    void OnTriggerExit(Collider other)
    {
        Parar();
        anim.SetBool("Atacando", false);
    }
    void Atacar()
    {
        Parar();
        anim.SetTrigger("Attack");
        anim.SetBool("Atacando",true);
        Slider_Personaje.value -= ataque;
    }
    public void setSlider(float damage)
    {
        Slider_Life.value -= damage;
    }
    public float getValueSlider()
    {
        return Slider_Life.value;
    }

}