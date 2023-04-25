using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SimpleEnemyBehaviour : DestructiveBase
{
    public float attack;
    public float distanceToFollow;
    public Animator animator;
    public float stunTime;

    private NavMeshAgent nmAgent;
    private PlayerBehaviour player;
    private float currentDistancePlayer;
    private Vector3 playerPosition; //Evita repetição da variável sem necessidade
    private bool inStun;
    private float currentStunTime;

    void Start(){
        nmAgent = GetComponent<NavMeshAgent>(); //Guarda os componentes do Inimigo no script
        player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour; //pega o player que está na Cena
        
    }

    void Update(){
        
        if (!inStun && !isDead){
            playerPosition = player.transform.position; //Evita processamento extra
            currentDistancePlayer = Vector3.Distance(transform.position, playerPosition); 

            if(currentDistancePlayer < distanceToFollow)
            {
                nmAgent.SetDestination(playerPosition);
            }

            animator.SetFloat("velocity", nmAgent.velocity.magnitude);
        }
        else
        {
            if (inStun)
            {
                currentStunTime += Time.deltaTime;

                if(currentStunTime > stunTime)
                {
                    currentStunTime = 0;
                    inStun = false;
                }
            }
        }

        animator.SetBool("inStun", inStun);
    }
    public override void OnDestroyed()
    {
        animator.SetTrigger("die");
        GetComponent<Collider>().enabled = false;
    }
    public override void OnApplyDamage()
    {
        inStun = true;
        animator.SetTrigger("hit");
    }
}
