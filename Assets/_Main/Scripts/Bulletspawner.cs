/************************************************************************************
\file       Bulletspawner.cs
\brief      This script ...
\author     #AUTHOR#
\date       #CREATIONDATE#
\copyright  © #YEAR#, #COMPANY# ApS. All rights reserved.
************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bulletspawner : MonoBehaviour
{
    //==============================================================================
    // Fields
    //==============================================================================
    public GameObject bullet;
    public Transform bulletspawnposition;
    public float force;
    public bool manualTestSpawnBullet = false;
    //==============================================================================
    // MonoBehaviour
    //==============================================================================
    private void Update() {
        if(manualTestSpawnBullet) {
            manualTestSpawnBullet = false;
            Spawnbullet();
        }
    }

    //==============================================================================
    // Public Methods
    //==============================================================================
    public void Spawnbullet()
    {
        GameObject spawnedbullet = Instantiate(bullet, bulletspawnposition.position, quaternion.identity);
        Rigidbody bulletrigidbody = spawnedbullet.GetComponent<Rigidbody>();
        bulletrigidbody.AddForce(-transform.right*force,ForceMode.Force);
        Destroy(spawnedbullet, 10);
    }

    //==============================================================================
    // Private Methods
    //==============================================================================
    

}
