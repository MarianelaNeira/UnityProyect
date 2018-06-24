using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    Animator animate;
    NavMeshAgent enemy;

    void Start()
    {
        animate = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();

        //Entrega valores random para el inicio y tiempo de repetición de código para cada enemigo
        float startTime = Random.Range(0f, 3f);
        float repeatRate = Random.Range(0f, 10f);

        //Repite método NewEndPosition cada repeatRate segundos
        InvokeRepeating("NewEndPosition", startTime, repeatRate);
    }

    void Update()
    {
        AnimationState();
    }

    void NewEndPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 15.0f; // Elige un pto random dentro de una esfera de una unidad de unity de radio * 15
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 15.0f, 1);
        Vector3 finalPosition = hit.position;
        enemy.SetDestination(finalPosition);
    }

    void AnimationState()
    {
        if (!enemy.pathPending)
        {
            if (enemy.remainingDistance <= enemy.stoppingDistance)
            {
                if (!enemy.hasPath || enemy.velocity.sqrMagnitude == 0f)
                {
                    animate.SetBool("Idle", true); //Si llega al destino cambia a animación Idle
                }
            }
            else
            {
                animate.SetBool("Idle", false); // Vuelve a la animación de correr
            }
        }
    }
}
