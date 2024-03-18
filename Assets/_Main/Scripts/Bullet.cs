/************************************************************************************
\file       Bullet.cs
\brief      This script ...
\author     #AUTHOR#
\date       #CREATIONDATE#
\copyright  © #YEAR#, #COMPANY# ApS. All rights reserved.
************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //==============================================================================
    // Fields
    //==============================================================================
    public LayerMask layerMask;

    ///    public int layerMask = 1 << 8;

    private Rigidbody rigidbody;
    private Vector3 positionLastFrame;
    //==============================================================================
    // MonoBehaviour
    //==============================================================================
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        positionLastFrame = transform.position;

    }

    void FixedUpdate()
    {

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;
        //Vector3 direction = rigidbody.velocity.normalized;
        Debug.DrawRay(transform.position, transform.position - positionLastFrame, Color.blue, 1f);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        //if (Physics.Raycast(transform.position, rigidbody.velocity.normalized, out hit, 0.1f, layerMask))
        if (Physics.Raycast(transform.position, transform.position - positionLastFrame, out hit, 0.1f, layerMask))
        {
            Debug.DrawRay(transform.position,  transform.position + (transform.position - positionLastFrame) * hit.distance, Color.yellow);
            if (hit.transform.GetComponentInParent<Runningmananimated>() != null)
            {
                hit.transform.GetComponentInParent<Runningmananimated>().EnableRagDoll();
                //Destroy(hit.transform.GetComponentInParent<Runningmananimated>().gameObject);
                Destroy(gameObject);
            }
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position,  transform.position - positionLastFrame, Color.white, 0.1f);
            Debug.Log("Did not Hit");
        }
        positionLastFrame = transform.position;

        //if(rigidbody.velocity.magnitude < 0.5f){
        //    Destroy(gameObject);
        //}
    }

    //==============================================================================
    // Public Methods
    //==============================================================================


    //==============================================================================
    // Private Methods
    //==============================================================================

}
