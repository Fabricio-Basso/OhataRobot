using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Panda;

public class AI : MonoBehaviour
{




    // position player
    public Transform player;   
    // position tiro
    public Transform bulletSpawn;
    // ref do bullet
    public GameObject bulletPrefab;
    // components do Slider
    public Slider healthBar;

    // Navmesh deste objeto
    NavMeshAgent agent;

    public Vector3 destination;
    public Vector3 target;

    //var para a vida 
    float health = 100.0f;
    
    //var veloc da rotação
    float rotSpeed = 5.0f;


    float shotRange = 40.0f;
    float visibleRange = 80.0f;

    void Start()
    {
        //pega os components do object
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = shotRange - 5; 

        //Funçao para mostrar vida
        InvokeRepeating("UpdateHealth",5,0.5f);
    }


    void Update()
    {
        //deixa a barra de hp em cima do boneco
        Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);

        healthBar.value = (int)health;
        healthBar.transform.position = healthBarPos + new Vector3(0,60,0);
    }

    void UpdateHealth()
    {
        //Regeneraçao
       if(health < 100)
        {
            health ++;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //removendo vida pela colisão
        if(col.gameObject.tag == "bullet")
        {
            health -= 10;
        }

    }

    [Task]
    public void PickRandomDestination()
    {
        //navegacao com Panda
        Vector3 dest = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        agent.SetDestination(dest);
        Task.current.Succeed();
    }

    [Task]
    public void MoveToDestination()
    {
        //deslocamento com Panda
        if(Task.isInspected)
            Task.current.debugInfo = string.Format("t={0:0.00}", Time.time);

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Task.current.Succeed();
        }

    }


}

