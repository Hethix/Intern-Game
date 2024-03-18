/************************************************************************************
\file       Runningmananimated.cs
\brief      This script ...
\author     #AUTHOR#
\date       #CREATIONDATE#
\copyright  © #YEAR#, #COMPANY# ApS. All rights reserved.
************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Runningmananimated : MonoBehaviour
{
    //==============================================================================
    // Fields
    //==============================================================================
    public float speed;
    private NavMeshAgent navMeshAgent;
    private Rigidbody[] rigidbodies;
    private Animator animator;
    private Coroutine findPlayerRoutine;
    private AudioSource audioSource;

    //==============================================================================
    // MonoBehaviour
    //==============================================================================
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        findPlayerRoutine = StartCoroutine(FindPlayerRoutine());
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
    }

    private void Update()
    {
    }

    //==============================================================================
    // Public Methods
    //==============================================================================
    public void EnableRagDoll()
    {
        StopCoroutine(findPlayerRoutine);
        animator.enabled = false;
        navMeshAgent.Stop();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
    }

    //==============================================================================
    // Private Methods
    //==============================================================================
    private void Attack()
    {
        Player player = Camera.main.GetComponent<Player>();
        player.Damagehealth();
        audioSource.Play();

    }


    private IEnumerator FindPlayerRoutine()
    {
        Vector3 targetPos;
        while (true)
        {
            targetPos = Camera.main.transform.position;
            targetPos.y = 0;
            navMeshAgent.destination = targetPos;
            if (Vector3.Distance(targetPos, transform.position) < 0.5f)
            {
                Attack();
            }
            yield return new WaitForSeconds(1f);

        }
    }
}
