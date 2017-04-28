using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Animator anim;
    Transform player;               // Reference to the player's position.
    //PlayerHealth playerHealth;      // Reference to the player's health.
    //EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.

    public Vector3 posPlayer;
    public float distancia;
    public float distanciaMax = 25f;

    void FixedUpdate()
    {

    }
    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
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
    void Update()
    {
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
}